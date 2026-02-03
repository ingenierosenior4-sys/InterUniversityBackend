using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IMateriaProfesorRepository
{
    Task<MateriaProfesor?> FindAsync(params object?[] keyValues);
    void Add(MateriaProfesor entity);
    Task<MateriaProfesor?> ObtenerClaseEstudiante(int materiaId, int profesorId, CancellationToken cancellationToken);
    Task<List<MateriaProfesor>> ObtenerMaterias(CancellationToken cancellationToken);
}
