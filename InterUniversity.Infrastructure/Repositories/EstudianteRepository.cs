using InterUniversity.Domain.Dtos;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using InterUniversity.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class EstudianteRepository(UniversidadDbContext dbContext) : Repository<Estudiante>(dbContext), IEstudianteRepository
{
    public Task<PagedResult<Estudiante>> ObtenerEstudiantesPaginado(int pageSize, int currentPage)
        => Entity
            .Include(e => e.EstudianteNavigation)
            .OrderBy(e => e.EstudianteNavigation!.Nombres)
            .GetPagedResultAsync(pageSize, currentPage);

    public Task<Estudiante?> ObtenerEstudiante(int estudianteId)
        => Entity
            .Include(e => e.EstudianteNavigation)
            .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

    public Task<bool> ExisteEstudiante(int estudianteId)
        => Entity.AnyAsync(e => e.EstudianteId == estudianteId);
}
