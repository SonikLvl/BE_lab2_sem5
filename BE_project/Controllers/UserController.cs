using BE_project.DTOs.User;
using BE_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost] // POST /user
        public ActionResult<UserDTO> CreateUser(CreateUserDTO userDto)
        {
            var newUser = _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = newUser.Id }, newUser); //201
        }

        [HttpGet("users")] // GET /user/users
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")] // GET /user/{id}
        public ActionResult<UserDTO> GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{userId}")] // DELETE /user/{id}
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent(); // 204
        }
    }
}
