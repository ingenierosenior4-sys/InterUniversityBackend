namespace UniversityApi.Features.Estudiantes.Queries.Get;

public class GetEstudiantesQueryResponse
{
    public string NumeroIdentificacion { get; init; } = "";
    public string Nombres { get; init; } = "";
    public string Apellidos { get; init; } = "";
    public DateTime FechaInscrito { get; init; }
}
