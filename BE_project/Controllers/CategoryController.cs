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

        [HttpPost] // POST /category?userId=1
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO categoryDTO, [FromQuery] int? userId)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryDTO, userId);
                return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id, UserId = userId }, createdCategory); //201
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request
            }
        }

        [HttpGet("allById")] // GET /category/allById?userId=1
        public async Task<ActionResult<List<CategoryDTO>>> GetCategory([FromQuery] int userId)
        {
            var categories = await _categoryService.GetAllCategoriesByUserAsync(userId);
            return Ok(categories); // 200 
        }
        [HttpGet("all")] // GET /category/all
        public async Task<ActionResult<List<CategoryDTO>>> GetCategory()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories); // 200 
        }

        [HttpGet("{categoryId}/{userId}")] // GET /category/{id}/{userid}
        public async Task<IActionResult> GetCategoryById(int categoryId, int userId)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId, userId);
                return Ok(category); // 200 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }
        [HttpGet("{userId}")] // GET /category?categoryName=name/{userid}
        public async Task<IActionResult> GetCategoryByName([FromQuery] string categoryName, int userId)
        {
            try
            {
                var category = await _categoryService.GetCategoryByNameAsync(categoryName, userId);
                return Ok(category); // 200 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }

        [HttpDelete("{categoryId}/{userId}")] // DELETE /category/{id}/{userid}
        public async Task<IActionResult> DeleteCategory(int categoryId, int userId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId, userId);
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
