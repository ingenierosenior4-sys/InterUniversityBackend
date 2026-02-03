using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class MateriaProfesorConfiguration : IEntityTypeConfiguration<MateriaProfesor>
{
    public void Configure(EntityTypeBuilder<MateriaProfesor> builder)
    {
        builder.HasKey(e => new { e.MateriaId, e.ProfesorId }).HasName("PK_MateriaProfesor_1");

        builder.ToTable("MateriaProfesor");

        builder.HasIndex(e => e.ProfesorId, "IX_MateriaProfesor_ProfesorId");

        builder.HasOne(d => d.Materia).WithMany(p => p.MateriaProfesors)
            .HasForeignKey(d => d.MateriaId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_MateriaProfesor_Materia");

        builder.HasOne(d => d.Profesor).WithMany(p => p.MateriaProfesors)
            .HasForeignKey(d => d.ProfesorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_MateriaProfesor_Profesor");
    }
}
