using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.Models;

namespace TaskProActive.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetAllByUserAsync(int userId);
        Task<TaskItem> GetByIdAsync(int id);
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Remove(TaskItem task);
    }
}