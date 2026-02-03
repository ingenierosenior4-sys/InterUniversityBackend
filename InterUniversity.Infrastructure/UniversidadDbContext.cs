using InterUniversity.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure;

public partial class UniversidadDbContext(DbContextOptions<UniversidadDbContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversidadDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
