using BE_project.Data.Repositories;
using BE_project.DTOs.User;
using BE_project.Models;

namespace BE_project.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO CreateUser(CreateUserDTO createUserDTO)
        {
            // More coming in lab3...

            var newUser = new User
            {
                Name = createUserDTO.Name
            };

            var savedUser = _userRepository.Add(newUser);

            return new UserDTO
            {
                Id = savedUser.Id,
                Name = savedUser.Name
            };
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            });
        }

        public UserDTO? GetUserById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
        }
    }
}
