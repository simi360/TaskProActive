using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.Data;
using TaskProActive.Models;

namespace TaskProActive.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public void Add(TaskItem task)
        {
            _context.TaskItems.Add(task);
        }

        public void Update(TaskItem task)
        {
            _context.TaskItems.Update(task);
        }

        public void Remove(TaskItem task)
        {
            _context.TaskItems.Remove(task);
        }
    }
}
