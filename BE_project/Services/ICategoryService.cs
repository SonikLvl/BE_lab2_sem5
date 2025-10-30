using BE_project.DTOs.Category;

namespace BE_project.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesByUserAsync(int userId);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId, int userId);
        Task<CategoryDTO> GetCategoryByNameAsync(string categoryName, int userId);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO, int? userId);
        Task DeleteCategoryAsync(int categoryId, int userId);
    }
}
