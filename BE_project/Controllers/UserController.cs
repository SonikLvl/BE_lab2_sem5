using BE_project.DTOs.User;
using BE_project.Exceptions;
using BE_project.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("register")] // POST /user/register
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO userDTO)
        {
            try
            {
                var newUser = await _userService.CreateUserAsync(userDTO);
                return CreatedAtAction(nameof(GetUserById), new { userId = newUser.Id }, newUser); //201
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
        }

        [HttpPost("login")] // POST /user/login
        [AllowAnonymous] 
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var token = await _userService.LoginAsync(loginDTO);
                return Ok(new { accessToken = token });
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

        [HttpGet("users")] // GET /user/users
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")] // GET /user/{id}
        public async Task<ActionResult<UserDTO>> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
        }

        [HttpDelete("{userId}")] // DELETE /user/{id}
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return NoContent(); // 204
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
        }
    }
}
