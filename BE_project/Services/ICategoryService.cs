using BE_project.DTOs.Category;

namespace BE_project.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task DeleteCategoryAsync(int categoryId);
    }
}
