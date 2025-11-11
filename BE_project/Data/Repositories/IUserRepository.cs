using BE_project.Models;
using System.Linq.Expressions;

namespace BE_project.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int userId);
        Task<User> AddAsync(User user);
        Task DeleteAsync(int userId);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
        Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate);
    }
}
