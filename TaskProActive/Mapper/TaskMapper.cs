using TaskProActive.Models;
using TaskProActive.DTO;

namespace TaskProActive.Mapper
{
    public static class TaskMapper
    {
        public static TaskDto ToDto(TaskItem task)
        {
            if (task == null) return new TaskDto();

            return new TaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Description = task.Description ?? String.Empty,
                Status = task.Status.ToString(),
                StatusId = (int)task.Status,
                Priority = task.Priority.ToString(),
                PriorityId = (int)task.Priority,
                CreatedOn = task.CreatedOn,
                CreatedBy = task.CreatedBy ?? 0,
                ModifiedOn = task.ModifiedOn ?? DateTime.MinValue,
                ModifiedBy = task.ModifiedBy ?? 0,
                Tags = task.TaskTags?.Select(tt => TagMapper.ToDto(tt.Tag)).ToList() ?? new System.Collections.Generic.List<TagDto>()
            };
        }

        public static TaskItem ToModel(TaskDto dto)
        {
            if (dto == null) return new TaskItem { Title = string.Empty };

            return new TaskItem
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                Status = Enum.Parse<TaskProActive.Models.TaskStatus>(dto.Status),
                Priority = Enum.Parse<TaskPriority>(dto.Priority),
                CreatedOn = dto.CreatedOn,
                CreatedBy = dto.CreatedBy,
                ModifiedOn = dto.ModifiedOn,
                ModifiedBy = dto.ModifiedBy,
            };
        }
    }
}
