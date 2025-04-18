using Amazon.DynamoDBv2.DataModel;
using MiniCore.API.Models;
using MiniCore.API.Repository.Interfaces;

namespace MiniCore.API.Repository;

public class ProjectRepository: IRepository<Project>
{
    private readonly IDynamoDBContext _context;
    
    public ProjectRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var conditions = new List<ScanCondition>();
        var allProyectos = await _context.ScanAsync<Project>(conditions).GetRemainingAsync();
        return allProyectos;
    }
    
    public async Task<Project> GetByIdAsync(string id)
    {
        return await _context.LoadAsync<Project>(id);
    }
    
    public async Task AddAsync(Project proyecto)
    {
        proyecto.Id = Guid.NewGuid().ToString();
        await _context.SaveAsync(proyecto);
    }
    
    public async Task UpdateAsync(string id, Project proyecto)
    {
        var existingProyecto = await GetByIdAsync(id);
        if (existingProyecto == null)
        {
            throw new Exception("Proyecto not found");
        }
        
        proyecto.Id = id;
        await _context.SaveAsync(proyecto);
    }
    
    public async Task DeleteAsync(string id)
    {
        var proyecto = await GetByIdAsync(id);
        if (proyecto == null)
        {
            throw new Exception("Proyecto not found");
        }
        
        await _context.DeleteAsync<Project>(id);
    }
}