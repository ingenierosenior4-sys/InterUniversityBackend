using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public abstract class Repository<TEntity>(UniversidadDbContext dbContext)
    where TEntity : class
{
    protected readonly UniversidadDbContext DbContext = dbContext;

    protected DbSet<TEntity> Entity => DbContext.Set<TEntity>();

    public Task<TEntity?> FindAsync(params object?[] keyValues) => Entity.FindAsync(keyValues).AsTask();

    public void Add(TEntity entity) => Entity.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) => Entity.AddRange(entities);

    public void Remove(TEntity entity) => Entity.Remove(entity);
}

