using BE_project.DTOs.Category;
using BE_project.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost] // POST /category
        public IActionResult CreateCategory(CreateCategoryDTO userDto)
        {
            return Ok();
        }

        [HttpGet] // GET /category
        public IActionResult GetCategory()
        {
            return Ok();
        }

        [HttpDelete("{categoryId}")] // DELETE /category/{id}
        public IActionResult DeleteCategory(int categoryId)
        {
            return Ok();
        }
    }
}
