using BE_project.DTOs.Category;
using BE_project.Models;
using BE_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost] // POST /category
        public ActionResult<CategoryDTO> CreateCategory(CreateCategoryDTO userDto)
        {
            var newCategory = _categoryService.CreateCategory(userDto);
            return CreatedAtAction(nameof(DeleteCategory), new { categoryId = newCategory.Id }, newCategory); //201
        }

        [HttpGet] // GET /category
        public ActionResult<List<CategoryDTO>> GetCategory()
        {
            var category = _categoryService.GetAllCategories();
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete("{categoryId}")] // DELETE /category/{id}
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryService.DeleteCategory(categoryId);
            return NoContent(); // 204
        }
    }
}
