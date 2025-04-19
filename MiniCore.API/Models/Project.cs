using Amazon.DynamoDBv2.DataModel;

namespace MiniCore.API.Models;

[DynamoDBTable("ProyectoCore")]
public class Project
{
    [DynamoDBHashKey("id")]
    public string Id { get; set; }
    
    [DynamoDBProperty]
    public string Nombre { get; set; }
    
    [DynamoDBProperty]
    public string Descripcion { get; set; }
    
    [DynamoDBProperty]
    public string FechaInicio { get; set; }
    
    [DynamoDBProperty]
    public string FechaFin { get; set; }
    
    [DynamoDBProperty]
    public List<string>? EmpleadosAsignados { get; set; } // IDs de los empleados
    
    [DynamoDBProperty]
    public List<Tasks>? Tareas { get; set; }
}