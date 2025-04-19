using Microsoft.AspNetCore.Mvc;
using MiniCore.API.Models;
using MiniCore.API.Repository.Interfaces;

namespace MiniCore.API.Controllers;

[Route("api/Project/{projectId}/tasks")]
[ApiController]
public class TasksController: ControllerBase
{
    private readonly ITasksRepository _tasksRepository;
    
    public TasksController(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTasks(string projectId)
    {
        var tasks = await _tasksRepository.GetTasksByProjectIdAsync(projectId);
        return Ok(tasks);
    }
    
    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTaskById(string projectId, string taskId)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(projectId, taskId);
        return Ok(task);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(string projectId, [FromBody] Tasks task)
    {
        await _tasksRepository.AddTaskAsync(projectId, task);
        return Ok("Task created successfully");
    }
    
    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(string projectId, string taskId, [FromBody] Tasks task)
    {
        await _tasksRepository.UpdateTaskAsync(projectId, taskId, task);
        return Ok("Task updated successfully");
    }
    
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(string projectId, string taskId)
    {
        await _tasksRepository.DeleteTaskAsync(projectId, taskId);
        return Ok("Task deleted successfully");
    }
}