using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public abstract class Repository<TEntity>(UniversidadDbContext dbContext)
    where TEntity : class
{
    protected readonly UniversidadDbContext DbContext = dbContext;

    protected DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    public Task<TEntity?> FindAsync(params object?[] keyValues) => Entities.FindAsync(keyValues).AsTask();

    public void Add(TEntity entity) => Entities.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) => Entities.AddRange(entities);

    public void Remove(TEntity entity) => Entities.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => DbContext.SaveChangesAsync(cancellationToken);
}

