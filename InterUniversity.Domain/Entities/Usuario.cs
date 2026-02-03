namespace InterUniversity.Domain.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NumeroIdentificacion { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Contrasena { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public virtual Estudiante? Estudiante { get; set; }

    public virtual Profesor? Profesor { get; set; }
}
