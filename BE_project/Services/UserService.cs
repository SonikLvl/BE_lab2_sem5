using BE_project.Data.Repositories;
using BE_project.DTOs.User;
using BE_project.Exceptions;
using BE_project.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_project.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Name))
            {
                throw new ValidationException("User name cannot be empty or whitespace.");
            }
            if (await _userRepository.AnyAsync(u => u.Name == createUserDTO.Name))
            {
                throw new ValidationException("Username already taken.");
            }
            if (string.IsNullOrWhiteSpace(createUserDTO.Password))
            {
                throw new ValidationException("Password cannot be empty.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);

            var newUser = new User
            {
                Name = createUserDTO.Name,
                PasswordHash = hashedPassword,
            };

            var savedUser = await _userRepository.AddAsync(newUser);

            return MapToUserDTO(savedUser);
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Name == loginDTO.Name);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new ValidationException("Invalid password.");
            }

            return GenerateJwtToken(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return users.Select(MapToUserDTO);
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            return MapToUserDTO(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var userToDelete = await _userRepository.GetByIdAsync(userId);
            if (userToDelete == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            await _userRepository.DeleteAsync(userId);
        }

        private UserDTO MapToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
