namespace TaskProActive.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = String.Empty;
       // public TaskDto Task { get; set; } = new TaskDto();
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}
