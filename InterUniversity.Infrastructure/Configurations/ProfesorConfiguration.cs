using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
{
    public void Configure(EntityTypeBuilder<Profesor> builder)
    {
        builder.HasKey(e => e.ProfesorId).HasName("PK_Profesor_1");

        builder.ToTable("Profesor");

        builder.Property(e => e.ProfesorId).ValueGeneratedNever();
        builder.Property(e => e.FechaContratacion).HasColumnType("smalldatetime");

        builder.HasOne(d => d.ProfesorNavigation).WithOne(p => p.Profesor)
            .HasForeignKey<Profesor>(d => d.ProfesorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Profesor_Usuario");
    }
}
