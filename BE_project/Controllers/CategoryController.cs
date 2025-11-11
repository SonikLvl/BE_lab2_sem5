using BE_project.DTOs.Category;
using BE_project.Exceptions;
using BE_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = GetCurrentUserId();

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

        [HttpGet("allById")] // GET /category/allById
        public async Task<ActionResult<List<CategoryDTO>>> GetCategoriesByUser()
        {
            var userId = GetCurrentUserId();
            var categories = await _categoryService.GetAllCategoriesByUserAsync(userId);
            return Ok(categories); // 200 
        }
        [HttpGet("all")] // GET /category/all
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories); // 200 
        }

        [HttpGet("{categoryId}")] // GET /category/{id}
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var userId = GetCurrentUserId();
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
        [HttpGet("")] // GET /category?categoryName=name
        public async Task<IActionResult> GetCategoryByName([FromQuery] string categoryName)
        {
            var userId = GetCurrentUserId();

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

        [HttpDelete("{categoryId}")] // DELETE /category/{id}
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var userId = GetCurrentUserId();

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
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Cannot find user ID claim in token.");
            }

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User ID claim is in an invalid format.");
        }
    }
}
