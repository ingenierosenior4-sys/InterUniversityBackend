using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class MateriaRepository(UniversidadDbContext dbContext) : Repository<Materia>(dbContext), IMateriaRepository
{
    public int ObtenerSumaCreditosPorMaterias(IEnumerable<int> idsMaterias)
        => Entity
            .Where(m => idsMaterias.Contains(m.MateriaId))
            .Sum(m => m.Creditos);
}
