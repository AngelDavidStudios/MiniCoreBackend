using MiniCore.API.Models;

namespace MiniCore.API.Repository.Interfaces;

public interface ITasksRepository
{
    Task<IEnumerable<Tasks>> GetTasksByProjectIdAsync(string projectId);
    Task<Tasks> GetTaskByIdAsync(string projectId, string taskId);
    Task AddTaskAsync(string projectId, Tasks task);
    Task UpdateTaskAsync(string projectId, string taskId, Tasks task);
    Task DeleteTaskAsync(string projectId, string taskId);
}