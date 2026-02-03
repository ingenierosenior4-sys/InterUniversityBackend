using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class UsuarioRepository(UniversidadDbContext dbContext) : Repository<Usuario>(dbContext), IUsuarioRepository
{
    public Task<Usuario?> ObtenerUsuarioEstudiante(int estudianteId)
        => Entities
            .Where(u => u.Estudiante != null && u.Estudiante.EstudianteId == estudianteId)
            .FirstOrDefaultAsync();

    public Task<bool> ExisteUsuarioEstudiante(string numeroIdentificacion, int excludeUsuarioId)
        => Entities.AnyAsync(u => u.NumeroIdentificacion == numeroIdentificacion && u.UsuarioId != excludeUsuarioId);

    public Task<bool> ExisteUsuario(string numeroIdentificacion)
        => Entities.AnyAsync(u => u.NumeroIdentificacion == numeroIdentificacion);

    public Task<Usuario?> ObtenerUsuario(string numeroIdentificacion)
        => Entities.FirstOrDefaultAsync(u => u.NumeroIdentificacion == numeroIdentificacion);
}
