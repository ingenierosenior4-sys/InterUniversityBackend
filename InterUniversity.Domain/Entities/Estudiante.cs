namespace InterUniversity.Domain.Entities;

public partial class Estudiante
{
    public int EstudianteId { get; set; }

    public DateTime FechaInscrito { get; set; }

    public byte Creditos { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual Usuario EstudianteNavigation { get; set; } = null!;
}
