using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IClaseRepository
{
    Task<Clase?> FindAsync(params object?[] keyValues);
    void Add(Clase entity);
    void AddRange(IEnumerable<Clase> entities);
    Task<List<Clase>> ObtenerClasesEstudiante(int estudianteId, CancellationToken cancellationToken);
    Task<string[]> ObtenerEstudiantes(int materiaId, int profesorId, CancellationToken cancellationToken);
}
