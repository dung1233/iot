using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TTS.Dto;
using TTS.Models.User;
using TTS.Service.Users;

namespace TTS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] Userdto userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User data cannot be null.");
            }
            if (string.IsNullOrEmpty(userdto.userName) || string.IsNullOrEmpty(userdto.Password))
            {
                return BadRequest("Username and password cannot be empty.");
            }
            if (await _userService.IsUsernameExistsAsync(userdto.userName))
            {
                return Conflict("Username already exists.");
            }
            try
            {
                var user = await _userService.CreateUser(userdto);
                return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto LoginDto)
        {
            if (LoginDto == null)
            {
                return BadRequest("Login data cannot be null.");
            }
            if (string.IsNullOrEmpty(LoginDto.UserName) || string.IsNullOrEmpty(LoginDto.Password))
            {
                return BadRequest("Username and password cannot be empty.");
            }
            try
            {
                var response = await _userService.LoginAsync(LoginDto.UserName, LoginDto.Password);
                if (response.Success)
                {
                    return Ok(response);
                }
                return Unauthorized(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [Authorize]
        [HttpGet("User")]

        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.GetAll();
            if (users == null)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

    } 
}
