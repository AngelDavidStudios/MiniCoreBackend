using Amazon.DynamoDBv2.DataModel;

namespace MiniCore.API.Models;

public class Tasks
{
    public string id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string FechaAsignada { get; set; }
    public string FechaLimite { get; set; } 
    public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;
}

public enum EstadoTarea
{
    Pendiente,
    EnProgreso,
    Completada,
    Retrasada
}