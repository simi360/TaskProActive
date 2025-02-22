using TaskProActive.Models;

namespace TaskProActive.DTO
{
    public class DashboardDto
    {
        public List<StatusCountDto> StatusCounts { get; set; }
        public List<PriorityCountDto> PriorityCounts { get; set; }
        public List<RecentTaskDto> RecentTasks { get; set; }
    }

    public class StatusCountDto
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class PriorityCountDto
    {
        public string Priority { get; set; }
        public int Count { get; set; }
    }

    public class RecentTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
