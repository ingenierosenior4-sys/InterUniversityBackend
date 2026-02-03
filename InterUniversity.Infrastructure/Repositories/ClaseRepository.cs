using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class ClaseRepository(UniversidadDbContext dbContext) : Repository<Clase>(dbContext), IClaseRepository
{
    public async Task<List<Clase>> ObtenerClasesEstudiante(int estudianteId, CancellationToken cancellationToken)
        => await Entity
               .Include(c => c.MateriaProfesor.Materia)
               .Include(c => c.MateriaProfesor.Profesor.ProfesorNavigation)
               .Where(c => c.EstudianteId == estudianteId)
               .ToListAsync(cancellationToken);

    public async Task<string[]> ObtenerEstudiantes(int materiaId, int profesorId, CancellationToken cancellationToken)
        => await Entity
               .Where(m => m.MateriaId == materiaId && m.ProfesorId == profesorId)
               .Include(m => m.Estudiante)
               .ThenInclude(e => e.EstudianteNavigation)
               .Select(m => $"{m.Estudiante.EstudianteNavigation.Nombres} {m.Estudiante.EstudianteNavigation.Apellidos}")
               .ToArrayAsync(cancellationToken);
}
