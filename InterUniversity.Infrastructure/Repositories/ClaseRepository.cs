using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class ClaseRepository(UniversidadDbContext dbContext) : Repository<Clase>(dbContext), IClaseRepository
{
    public IQueryable<Clase> ObtenerClasesEstudiante(int estudianteId)
        => Entities.Where(c => c.EstudianteId == estudianteId);

    public IQueryable<Clase> ObtenerClaseEstudiantes(int materiaId, int profesorId)
        => Entities.Where(m => m.MateriaId == materiaId && m.ProfesorId == profesorId)
            .Include(m => m.Estudiante)
            .ThenInclude(e => e.EstudianteNavigation);
}
