using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskProActive.Services;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
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

    [HttpGet]
    public async Task<IActionResult> GetDashboardData()
    {
        int userId = GetCurrentUserId();
        var dashboardData = await _dashboardService.GetDashboardData(userId);
        return Ok(dashboardData);
    }
}
