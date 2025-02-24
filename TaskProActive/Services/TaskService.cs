using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProActive.Data;
using TaskProActive.DTO;
using TaskProActive.Mapper;
using TaskProActive.Models;
using TaskProActive.Repositories;

namespace TaskProActive.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;

        public TaskService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _uow = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _uow.TaskRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(int userId)
        {
            return await _uow.TaskRepository.GetAllByUserAsync(userId);
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _uow.TaskRepository.GetByIdAsync(id);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, int currentUserId)
        {
            var taskModel = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = Enum.Parse<TaskProActive.Models.TaskStatus>(createTaskDto.Status),
                Priority = Enum.Parse<TaskPriority>(createTaskDto.Priority),
                UserId = currentUserId,
                CreatedBy = currentUserId,
                CreatedOn = DateTime.UtcNow,
            };

            if (createTaskDto.Tags != null && createTaskDto.Tags.Any())
            {
                taskModel.TaskTags = await ProcessTagsAsync(taskModel, createTaskDto.Tags, currentUserId);
            }

            _uow.TaskRepository.Add(taskModel);
            await _uow.CompleteAsync();

            return TaskMapper.ToDto(taskModel);
        }

        public async Task UpdateTaskAsync(int id, TaskItem updatedTask, int currentUserId, List<TagDto> tagDtos = null)
        {
            var task = await GetTaskByIdAsync(id);
            if (task == null)
                return;

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;
            task.Priority = updatedTask.Priority;

            task.ModifiedOn = DateTime.UtcNow;
            task.ModifiedBy = currentUserId;

            if (tagDtos != null)
            {
                task.TaskTags.Clear();
                task.TaskTags = await ProcessTagsAsync(task, tagDtos, currentUserId);
            }

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

        private async Task<ICollection<TaskTag>> ProcessTagsAsync(TaskItem task, List<TagDto> tagDtos, int currentUserId)
        {
            var taskTags = new List<TaskTag>();
            foreach (var tagDto in tagDtos)
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t =>
                    t.Name.ToLower() == tagDto.Name.ToLower() &&
                    t.UserId == currentUserId);
                if (tag == null)
                {
                    tag = new Tag
                    {
                        Name = tagDto.Name,
                        UserId = currentUserId,
                        CreatedBy = currentUserId,
                        CreatedOn = DateTime.UtcNow
                    };
                    await _context.Tags.AddAsync(tag);
                    await _context.SaveChangesAsync();
                }
                taskTags.Add(new TaskTag { TaskItem = task, Tag = tag });
            }
            return taskTags;
        }

    }
}
