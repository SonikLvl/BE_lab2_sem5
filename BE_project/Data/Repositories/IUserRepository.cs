using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int userId);
        Task<User> AddAsync(User user);
        Task DeleteAsync(int userId);
    }
}
