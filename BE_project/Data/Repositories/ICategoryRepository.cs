using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByNameAsync(string categoryId);
        Task<Category?> GetByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task DeleteAsync(int categoryId);
    }
}
