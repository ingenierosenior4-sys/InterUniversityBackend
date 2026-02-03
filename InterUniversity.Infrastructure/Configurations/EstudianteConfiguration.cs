using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
{
    public void Configure(EntityTypeBuilder<Estudiante> builder)
    {
        builder.HasKey(e => e.EstudianteId).HasName("PK_Estudiante_1");

        builder.ToTable("Estudiante");

        builder.Property(e => e.EstudianteId).ValueGeneratedNever();
        builder.Property(e => e.FechaInscrito).HasColumnType("smalldatetime");

        builder.HasOne(d => d.EstudianteNavigation).WithOne(p => p.Estudiante)
            .HasForeignKey<Estudiante>(d => d.EstudianteId)
            .HasConstraintName("FK_Estudiante_Usuario");
    }
}
