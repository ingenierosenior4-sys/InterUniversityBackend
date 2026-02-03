using InterUniversity.Domain.Entities;

namespace InterUniversity.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> FindAsync(params object?[] keyValues);
    Task<Usuario?> ObtenerUsuarioEstudiante(int estudianteId);
    Task<bool> ExisteUsuarioEstudiante(string numeroIdentificacion, int excludeUsuarioId);
    Task<Usuario?> ObtenerUsuario(string numeroIdentificacion);
    void Add(Usuario entity);
    void Remove(Usuario entity);
    Task<bool> ExisteUsuario(string numeroIdentificacion);
}
