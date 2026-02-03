using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IClaseRepository
{
    Task<Clase?> FindAsync(params object?[] keyValues);
    void Add(Clase entity);
    void AddRange(IEnumerable<Clase> entities);
    IQueryable<Clase> ObtenerClasesEstudiante(int estudianteId);
    IQueryable<Clase> ObtenerClaseEstudiantes(int materiaId, int profesorId);
}
