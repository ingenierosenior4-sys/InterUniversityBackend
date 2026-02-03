using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class ClaseConfiguration : IEntityTypeConfiguration<Clase>
{
    public void Configure(EntityTypeBuilder<Clase> builder)
    {
        builder.HasKey(e => new { e.ProfesorId, e.EstudianteId });

        builder.ToTable("Clase");

        builder.HasIndex(e => e.EstudianteId, "IX_Clase_EstudianteId");

        builder.HasIndex(e => new { e.MateriaId, e.ProfesorId }, "IX_Clase_MateriaId_ProfesorId");

        builder.HasOne(d => d.Estudiante).WithMany(p => p.Clases)
            .HasForeignKey(d => d.EstudianteId)
            .HasConstraintName("FK_Clase_Estudiante");

        builder.HasOne(d => d.MateriaProfesor).WithMany(p => p.Clases)
            .HasForeignKey(d => new { d.MateriaId, d.ProfesorId })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Clase_MateriaProfesor");
    }
}
