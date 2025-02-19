using System.Collections.Generic;
using System.Threading.Tasks;
using TaskProActive.Models;
using TaskProActive.Repositories;

namespace TaskProActive.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _uow;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _uow.TaskRepository.GetAllAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _uow.TaskRepository.GetByIdAsync(id);
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            _uow.TaskRepository.Add(task);
            await _uow.CompleteAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _uow.TaskRepository.Update(task);
            await _uow.CompleteAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _uow.TaskRepository.GetByIdAsync(id);
            if (task != null)
            {
                _uow.TaskRepository.Remove(task);
                await _uow.CompleteAsync();
            }
        }
    }
}
