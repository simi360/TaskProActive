namespace TaskProActive.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public UserDto User { get; set; } = new UserDto();
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public int StatusId { get; set; }
        public string Priority { get; set; } = String.Empty;
        public int PriorityId { get; set; }
        public List<TagDto> Tags { get; set; } = [];
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }

    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Minor"; 
        public List<TagDto> Tags { get; set; } = [];
    }
}
