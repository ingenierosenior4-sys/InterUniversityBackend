using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IMateriaRepository
{
    Task<Materia?> FindAsync(params object?[] keyValues);
    void Add(Materia entity);
    int ObtenerSumaCreditosPorMaterias(IEnumerable<int> idsMaterias);
}
