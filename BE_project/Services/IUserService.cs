using BE_project.DTOs.User;

namespace BE_project.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO? GetUserById(int userId);
        UserDTO CreateUser(CreateUserDTO createUserDTO);
        void DeleteUser(int userId);
    }
}
