using BE_project.Data.Repositories;
using BE_project.DTOs.Category;
using BE_project.Exceptions;
using BE_project.Models;

namespace BE_project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRecordRepository _recordRepository; 

        public CategoryService(ICategoryRepository categoryRepository, IRecordRepository recordRepository)
        {
            _categoryRepository = categoryRepository;
            _recordRepository = recordRepository;
        }
        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            if (string.IsNullOrWhiteSpace(createCategoryDTO.CategoryName))
            {
                throw new ValidationException("Category name cannot be empty or whitespace.");
            }

            var existingCategory = await _categoryRepository.GetByNameAsync(createCategoryDTO.CategoryName);
            if (existingCategory != null)
            {
                throw new ValidationException($"A category with the name '{createCategoryDTO.CategoryName}' already exists.");
            }

            var newCategory = new Category
            {
                CategoryName = createCategoryDTO.CategoryName
            };

            var savedCategory = await _categoryRepository.AddAsync(newCategory); 

            return MapToCategoryDTO(savedCategory);
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(categoryId);
            if (categoryToDelete == null)
            {
                throw new NotFoundException($"Category with ID {categoryId} not found.");
            }

            var relatedRecords = await _recordRepository.GetFilteredAsync(userId: null, categoryId: categoryId);
            if (relatedRecords.Any())
            {
                throw new ValidationException("Cannot delete category. It is associated with one or more records. Please delete or re-assign records first.");
            }

            await _categoryRepository.DeleteAsync(categoryId);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException($"Category with ID {categoryId} not found.");
            }

            return MapToCategoryDTO(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToCategoryDTO);
        }

        private CategoryDTO MapToCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
        }
    }
}
