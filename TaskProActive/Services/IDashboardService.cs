using TaskProActive.DTO;

namespace TaskProActive.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardData(int userId);
    }
}
