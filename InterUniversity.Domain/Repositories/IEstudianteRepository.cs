using InterUniversity.Domain.Dtos;
using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IEstudianteRepository
{
    Task<Estudiante?> FindAsync(params object?[] keyValues);
    void Add(Estudiante entity);
    Task<PagedResult<Estudiante>> ObtenerEstudiantesPaginado(int pageSize, int currentPage);
    Task<Estudiante?> ObtenerEstudiante(int estudianteId);
    Task<bool> ExisteEstudiante(int estudianteId);
}
