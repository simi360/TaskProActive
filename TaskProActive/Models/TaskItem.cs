namespace TaskProActive.Models
{
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Minor,
        Major,
        Critical,
        Blocker
    }

    public class TaskItem: AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Title { get; set; } = String.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public TaskPriority Priority { get; set; } = TaskPriority.Minor;

        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
