using BE_project.Models;

namespace BE_project.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByNameAsync(string categoryName, int? userId);
        Task<Category?> GetByIdAsync(int categoryId, int? userId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetCategoriesForUserAsync(int userId);
        Task<Category> AddAsync(Category category);
        Task DeleteAsync(int categoryId);
    }
}
