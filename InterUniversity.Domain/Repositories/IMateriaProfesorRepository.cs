using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IMateriaProfesorRepository
{
    Task<MateriaProfesor?> FindAsync(params object?[] keyValues);
    void Add(MateriaProfesor entity);
    IQueryable<MateriaProfesor> ObtenerClaseEstudiante(int materiaId, int profesorId);
    IQueryable<MateriaProfesor> ObtenerMaterias();
}
