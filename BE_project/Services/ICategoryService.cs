using BE_project.DTOs.Category;

namespace BE_project.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO CreateCategory(CreateCategoryDTO createCategoryDTO);
        void DeleteCategory(int categoryId);
    }
}
