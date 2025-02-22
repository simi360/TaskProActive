using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskProActive.Data;
using TaskProActive.DTO;
using TaskProActive.Mapper;

namespace TaskProActive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
            if (claim == null)
                throw new Exception("User id claim not found.");
            return int.Parse(claim.Value);
        }

        //TODO: Need to change
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int currentUserId = GetCurrentUserId();
            var tags = await _context.Tags
                .Where(t => t.UserId == currentUserId)
                .ToListAsync();
            var tagDtos = tags.Select(t => TagMapper.ToDto(t));
            return Ok(tagDtos);
        }
    }
}
