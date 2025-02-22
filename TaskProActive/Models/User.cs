namespace TaskProActive.Models
{
    public class User: AuditableEntity
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Name { get; set; }
        public string Role { get; set; } = String.Empty;
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
