using Amazon.DynamoDBv2.DataModel;
using MiniCore.API.Models;
using MiniCore.API.Repository.Interfaces;

namespace MiniCore.API.Repository;

public class TasksRepository: ITasksRepository
{
    private readonly IDynamoDBContext _context;
    
    public TasksRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Tasks>> GetTasksByProjectIdAsync(string projectId)
    {
        var project = await _context.LoadAsync<Project>(projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        return project.Tareas ?? new List<Tasks>();
    }
    
    public async Task<Tasks> GetTaskByIdAsync(string projectId, string taskId)
    {
        var project = await _context.LoadAsync<Project>(projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var task = project.Tareas?.FirstOrDefault(t => t.id == taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        return task;
    }
    
    public async Task AddTaskAsync(string projectId, Tasks task)
    {
        var project = await _context.LoadAsync<Project>(projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        task.id = Guid.NewGuid().ToString();
        project.Tareas ??= new List<Tasks>();
        project.Tareas.Add(task);
        
        await _context.SaveAsync(project);
    }
    
    public async Task UpdateTaskAsync(string projectId, string taskId, Tasks task)
    {
        var project = await _context.LoadAsync<Project>(projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var existingTask = project.Tareas?.FirstOrDefault(t => t.id == taskId);
        if (existingTask == null)
        {
            throw new Exception("Task not found");
        }

        existingTask.Titulo = task.Titulo;
        existingTask.Descripcion = task.Descripcion;
        existingTask.FechaAsignada = task.FechaAsignada;
        existingTask.FechaLimite = task.FechaLimite;
        existingTask.Estado = task.Estado;

        await _context.SaveAsync(project);
    }
    
    public async Task DeleteTaskAsync(string projectId, string taskId)
    {
        var project = await _context.LoadAsync<Project>(projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var task = project.Tareas?.FirstOrDefault(t => t.id == taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        project.Tareas.Remove(task);

        await _context.SaveAsync(project);
    }
}