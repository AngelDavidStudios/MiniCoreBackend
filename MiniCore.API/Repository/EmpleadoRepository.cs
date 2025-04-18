using Amazon.DynamoDBv2.DataModel;
using MiniCore.API.Models;
using MiniCore.API.Repository.Interfaces;

namespace MiniCore.API.Repository;

public class EmpleadoRepository: IRepository<Empleado>
{
    private readonly IDynamoDBContext _context;

    public EmpleadoRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        var conditions = new List<ScanCondition>();
        var allEmpleados = await _context.ScanAsync<Empleado>(conditions).GetRemainingAsync();
        return allEmpleados;
    }
    
    public async Task<Empleado> GetByIdAsync(string id)
    {
        return await _context.LoadAsync<Empleado>(id);
    }
    
    public async Task AddAsync(Empleado empleado)
    {
        empleado.Id = Guid.NewGuid().ToString();
        await _context.SaveAsync(empleado);
    }
    
    public async Task UpdateAsync(string id, Empleado empleado)
    {
        var existingEmpleado = await GetByIdAsync(id);
        if (existingEmpleado == null)
        {
            throw new Exception("Empleado not found");
        }
        
        empleado.Id = id;
        await _context.SaveAsync(empleado);
    }
    
    public async Task DeleteAsync(string id)
    {
        var empleado = await GetByIdAsync(id);
        if (empleado == null)
        {
            throw new Exception("Empleado not found");
        }
        
        await _context.DeleteAsync<Empleado>(id);
    }
}