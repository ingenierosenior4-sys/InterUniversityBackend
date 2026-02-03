using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class MateriaProfesorRepository(UniversidadDbContext dbContext) : Repository<MateriaProfesor>(dbContext), IMateriaProfesorRepository
{
    public async Task<MateriaProfesor?> ObtenerClaseEstudiante(int materiaId, int profesorId, CancellationToken cancellationToken)
        => await Entity
            .FirstOrDefaultAsync(m => m.MateriaId == materiaId && m.ProfesorId == profesorId, cancellationToken);

    public async Task<List<MateriaProfesor>> ObtenerMaterias(CancellationToken cancellationToken)
        => await Entity
            .Include(mf => mf.Materia)
            .Include(mf => mf.Profesor.ProfesorNavigation)
            .OrderBy(e => e.Materia.Titulo)
            .ToListAsync(cancellationToken);
}
