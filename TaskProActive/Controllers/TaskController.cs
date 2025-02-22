using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskProActive.DTO;
using TaskProActive.Mapper;
using TaskProActive.Models;
using TaskProActive.Services;

namespace TaskPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
            if (claim == null)
                throw new Exception("User id claim not found.");
            return int.Parse(claim.Value);
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int currentUserId = GetCurrentUserId();
            var tasks = await _taskService.GetAllTasksAsync(currentUserId);
            var tasksDto = tasks.Select(t => TaskMapper.ToDto(t));
            return Ok(tasksDto);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();
            return Ok(TaskMapper.ToDto(task));
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto)
        {
            int currentUserId = GetCurrentUserId();
            var createdTaskDto = await _taskService.CreateTaskAsync(createTaskDto, currentUserId);
            return CreatedAtAction(nameof(GetById), new { id = createdTaskDto.Id }, createdTaskDto);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskDto updatedTaskDto)
        {
            int currentUserId = GetCurrentUserId();
            var updatedTask = TaskMapper.ToModel(updatedTaskDto);
            // Ensure the service sets ModifiedOn and ModifiedBy
            await _taskService.UpdateTaskAsync(id, updatedTask, currentUserId, updatedTaskDto.Tags);
            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
