using TaskProActive.Models;

namespace TaskProActive.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem> GetByIdAsync(int id);
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Remove(TaskItem task);
    }
}
