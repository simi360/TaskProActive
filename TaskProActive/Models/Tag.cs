using System.ComponentModel.DataAnnotations.Schema;

namespace TaskProActive.Models
{
    public class Tag : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        [ForeignKey("Owner")]
        public int UserId { get; set; }
        public ICollection<TaskTag> TaskTags { get; set; }
        public virtual User Owner { get; set; }
    }
}
