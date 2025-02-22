using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProActive.DTO;
using TaskProActive.Models;
using TaskProActive.Repositories;
using TaskProActive.Services;

public class DashboardService : IDashboardService
{
    private readonly ITaskRepository _taskRepository;

    public DashboardService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<DashboardDto> GetDashboardData(int userId)
    {
        var tasks = await _taskRepository.GetAllByUserAsync(userId);

        var statusCounts = tasks
            .GroupBy(t => t.Status)
            .Select(g => new StatusCountDto
            {
                Status = g.Key.ToString(),  
                Count = g.Count()
            })
            .ToList();

        var priorityCounts = tasks
            .Where(t => t.Status != TaskProActive.Models.TaskStatus.Completed)
            .GroupBy(t => t.Priority)
            .Select(g => new PriorityCountDto
            {
                Priority = g.Key.ToString(),  
                Count = g.Count()
            })
            .ToList();

        var recentTasks = tasks
            .Where(t => t.ModifiedOn != null)
            .OrderByDescending(t => t.ModifiedOn)
            .Take(10)
            .Select(t => new RecentTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status.ToString(),  
                Priority = t.Priority.ToString(),  
                ModifiedOn = t.ModifiedOn
            })
            .ToList();

        return new DashboardDto
        {
            StatusCounts = statusCounts,
            PriorityCounts = priorityCounts,
            RecentTasks = recentTasks
        };
    }
}
