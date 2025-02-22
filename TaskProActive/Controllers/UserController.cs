using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskProActive.DTO;
using TaskProActive.Services;

namespace TaskProActive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
            if (userIdClaim == null)
            {
                return 0;
            }
            return int.Parse(userIdClaim.Value);
        }

        // POST: api/users/registerByAdmin
        [HttpPost("registerByAdmin")]
        public async Task<IActionResult> RegisterByAdmin([FromBody] RegisterRequestDto request)
        {
            int adminUserId = GetCurrentUserId();
            if (adminUserId <= 0)
                return Unauthorized("User id missing in token.");

            var result = await _userService.RegisterUserByAdminAsync(request, adminUserId);
            if (!result)
                return BadRequest("User already exists or registration failed.");

            return Ok("User registered successfully by admin.");
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto updatedUser)
        {
            int modifierUserId = GetCurrentUserId();
            if (modifierUserId <= 0)
                return Unauthorized("User id missing in token.");

            var result = await _userService.UpdateUserAsync(id, updatedUser, modifierUserId);
            if (!result)
                return NotFound("User not found.");
            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("User not found.");
            return NoContent();
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound();
            return Ok(userDto);
        }
    }
}
