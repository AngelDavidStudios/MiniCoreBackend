using Amazon.DynamoDBv2.DataModel;

namespace MiniCore.API.Models;

[DynamoDBTable("Task")]
public class Task
{
    [DynamoDBHashKey("id")]
    public string Id { get; set; }
    
    [DynamoDBProperty]
    public string Titulo { get; set; }
    
    [DynamoDBProperty]
    public string Descripcion { get; set; }
    
    [DynamoDBProperty]
    public string FechaAsignada { get; set; }
    
    [DynamoDBProperty]
    public string FechaLimite { get; set; }

    [DynamoDBProperty] public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;
}

public enum EstadoTarea
{
    Pendiente,
    EnProgreso,
    Completada,
    Retrasada
}