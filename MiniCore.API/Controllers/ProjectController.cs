using Microsoft.AspNetCore.Mvc;
using MiniCore.API.Models;
using MiniCore.API.Repository.Interfaces;

namespace MiniCore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController: ControllerBase
{
    private readonly IRepository<Project> _proyectoRepository;
    
    public ProjectController(IRepository<Project> proyectoRepository)
    {
        _proyectoRepository = proyectoRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var proyectos = await _proyectoRepository.GetAllAsync();
        return Ok(proyectos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var proyecto = await _proyectoRepository.GetByIdAsync(id);
        return Ok(proyecto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Project proyecto)
    {
        if (proyecto.Id != null)
        {
            proyecto.Id = null;
        }
        await _proyectoRepository.AddAsync(proyecto);
        return Ok("Proyecto created successfully");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Project proyecto)
    {
        await _proyectoRepository.UpdateAsync(id, proyecto);
        return Ok("Proyecto updated successfully");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _proyectoRepository.DeleteAsync(id);
        return Ok("Proyecto deleted successfully");
    }
}