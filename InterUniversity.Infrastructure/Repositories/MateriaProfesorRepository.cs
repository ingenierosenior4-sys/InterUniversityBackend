using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class MateriaProfesorRepository(UniversidadDbContext dbContext) : Repository<MateriaProfesor>(dbContext), IMateriaProfesorRepository
{
    public IQueryable<MateriaProfesor> ObtenerClaseEstudiante(int materiaId, int profesorId)
        => Entities.Where(m => m.MateriaId == materiaId && m.ProfesorId == profesorId);

    public IQueryable<MateriaProfesor> ObtenerMaterias()
        => Entities.OrderBy(e => e.Materia.Titulo);
}
