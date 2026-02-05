using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class UsuarioRepository(UniversidadDbContext dbContext) : Repository<Usuario>(dbContext), IUsuarioRepository
{
    public Task<Usuario?> ObtenerUsuarioEstudiante(int estudianteId)
        => Entity.FirstOrDefaultAsync(u => u.Estudiante != null && u.Estudiante.EstudianteId == estudianteId);

    public Task<bool> ExisteUsuarioEstudiante(string numeroIdentificacion, int excludeUsuarioId)
        => Entity.AnyAsync(u => u.NumeroIdentificacion == numeroIdentificacion && u.UsuarioId != excludeUsuarioId);

    public Task<bool> ExisteUsuario(string numeroIdentificacion)
        => Entity.AnyAsync(u => u.NumeroIdentificacion == numeroIdentificacion);

    public Task<Usuario?> ObtenerUsuario(string numeroIdentificacion)
        => Entity.FirstOrDefaultAsync(u => u.NumeroIdentificacion == numeroIdentificacion);
}
