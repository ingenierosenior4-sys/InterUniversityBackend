namespace InterUniversity.Application.Features.Clases.Queries.GetClase;

public sealed class GetClaseQueryResponse
{
    public int MateriaId { get; init; }
    public int ProfesorId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Profesor { get; init; } = string.Empty;
    public string[] Estudiantes { get; set; } = [];
}