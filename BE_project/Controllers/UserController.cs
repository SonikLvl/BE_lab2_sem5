using BE_project.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost] // POST /user
        public IActionResult CreateUser(CreateUserDTO userDto)
        {
            return Ok();
        }

        [HttpGet("users")] // GET /user/users
        public IActionResult GetUsers()
        {
            return Ok();
        }

        [HttpGet("{userId}")] // GET /user/{id}
        public IActionResult GetUserById(int userId)
        {
            return Ok();
        }

        [HttpDelete("{userId}")] // DELETE /user/{id}
        public IActionResult DeleteUser(int userId)
        {
            return Ok();
        }
    }
}
