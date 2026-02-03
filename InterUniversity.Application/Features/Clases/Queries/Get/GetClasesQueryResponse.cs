namespace InterUniversity.Application.Features.Clases.Queries.Get;

public sealed class GetClasesQueryResponse
{
    public int MateriaId { get; init; }
    public int ProfesorId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Profesor { get; init; } = string.Empty;
}
