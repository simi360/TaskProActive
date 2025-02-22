using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.DTO;
using TaskProActive.Models;

namespace TaskProActive.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(int userId);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto task, int currentUserId);
        Task UpdateTaskAsync(int id, TaskItem updatedTask, int currentUserId, List<TagDto> tagDtos = null);
        Task DeleteTaskAsync(int id);
    }
}
