using BE_project.Data.Repositories;
using BE_project.DTOs.Category;
using BE_project.Models;

namespace BE_project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public CategoryDTO CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var newCategory = new Category
            {
                CategoryName = createCategoryDTO.CategoryName
            };

            var savedCategory = _categoryRepository.Add(newCategory);

            return new CategoryDTO
            {
                Id = savedCategory.Id,
                CategoryName = savedCategory.CategoryName
            };
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();

            return categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            });
        }
    }
}
