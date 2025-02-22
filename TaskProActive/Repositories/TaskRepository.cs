using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.Models;
using TaskProActive.Data;
using TaskProActive.Repositories;

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
            return await _context.TaskItems.Include(t => t.TaskTags)
                                           .ThenInclude(tt => tt.Tag)
                                           .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserAsync(int userId)
        {
            return await _context.TaskItems
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.TaskItems
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t => t.Id == id);
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
