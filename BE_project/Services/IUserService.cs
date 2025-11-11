using BE_project.DTOs.User;

namespace BE_project.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task DeleteUserAsync(int userId);
    }
}
