namespace InterUniversity.Application.Features.Materias.Queries.Get;

public sealed class GetMateriasQueryResponse
{
    public int MateriaId { get; init; }
    public int ProfesorId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public byte Creditos { get; init; }
    public string Profesor { get; init; } = string.Empty;
}
