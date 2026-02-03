namespace UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

public sealed class GetEstudianteQueryResponse
{
    public int EstudianteId { get; set; }
    public string NumeroIdentificacion { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public byte Creditos { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public DateTime FechaInscrito { get; set; }
}
