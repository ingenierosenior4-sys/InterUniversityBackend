using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface ICreditoRepository
{
    Task<Credito?> FindAsync(params object?[] keyValues);
    void Add(Credito entity);
}
