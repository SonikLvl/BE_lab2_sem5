using BE_project.Data.Repositories;
using BE_project.DTOs.User;
using BE_project.Exceptions;
using BE_project.Models;

namespace BE_project.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Name))
            {
                throw new ValidationException("User name cannot be empty or whitespace.");
            }

            var newUser = new User
            {
                Name = createUserDTO.Name
            };

            var savedUser = await _userRepository.AddAsync(newUser);

            return MapToUserDTO(savedUser);
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
    }
}
