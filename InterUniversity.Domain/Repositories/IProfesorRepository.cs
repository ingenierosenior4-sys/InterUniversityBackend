using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IProfesorRepository
{
    Task<Profesor?> FindAsync(params object?[] keyValues);
    void Add(Profesor entity);
}