namespace InterUniversity.Domain.Entities;

public partial class Profesor
{
    public int ProfesorId { get; set; }

    public DateTime FechaContratacion { get; set; }

    public virtual ICollection<MateriaProfesor> MateriaProfesors { get; set; } = new List<MateriaProfesor>();

    public virtual Usuario ProfesorNavigation { get; set; } = null!;
}
