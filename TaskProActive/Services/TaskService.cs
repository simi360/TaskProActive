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
                // Tags will be processed below
            };

            // Process tags: the service should handle adding new tags and linking existing tags.
            if (createTaskDto.Tags != null && createTaskDto.Tags.Any())
            {
                taskModel.TaskTags = await ProcessTagsAsync(taskModel, createTaskDto.Tags, currentUserId);
            }

            // Add the task using repository
            _uow.TaskRepository.Add(taskModel);
            await _uow.CompleteAsync();

            // Return a mapped TaskDto (using your manual mapper)
            return TaskMapper.ToDto(taskModel);
        }

        public async Task UpdateTaskAsync(int id, TaskItem updatedTask, int currentUserId, List<TagDto> tagDtos = null)
        {
            var task = await GetTaskByIdAsync(id);
            if (task == null)
                return;

            // Update fields
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;
            task.Priority = updatedTask.Priority;

            // Update audit fields
            task.ModifiedOn = DateTime.UtcNow;
            task.ModifiedBy = currentUserId;

            // Process tags if provided: replace existing TaskTags with new ones.
            if (tagDtos != null)
            {
                // Clear existing tags
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
                    // Create new tag if not found
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
