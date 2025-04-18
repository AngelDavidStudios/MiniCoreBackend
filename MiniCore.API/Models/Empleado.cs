using Amazon.DynamoDBv2.DataModel;

namespace MiniCore.API.Models;

[DynamoDBTable("EmpleadoCore")]
public class Empleado
{
    [DynamoDBHashKey("id")]
    public string Id { get; set; }
    
    [DynamoDBProperty]
    public string Nombre { get; set; }
    
    [DynamoDBProperty]
    public string Correo { get; set; }
    
    [DynamoDBProperty]
    public List<string> ProyectosAsignados { get; set; } // IDs de los proyectos
}