using BE_project.DTOs.Category;
using BE_project.Exceptions;
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
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryDTO);
                return CreatedAtAction(nameof(DeleteCategory), new { categoryId = createdCategory.Id }, createdCategory); //201
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request
            }
        }

        [HttpGet] // GET /category
        public async Task<ActionResult<List<CategoryDTO>>> GetCategory()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories); // 200 
        }

        [HttpGet("{categoryId}")] // GET /category/{id}
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);
                return Ok(category); // 200 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }

        [HttpDelete("{categoryId}")] // DELETE /category/{id}
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 
            }
        }
    }
}
