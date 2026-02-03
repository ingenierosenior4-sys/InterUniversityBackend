using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        builder.HasIndex(e => e.NumeroIdentificacion, "UQ__Usuario__FCA68D9105274D61").IsUnique();

        builder.Property(e => e.Apellidos).HasMaxLength(200);
        builder.Property(e => e.Contrasena).HasMaxLength(100);
        builder.Property(e => e.Nombres).HasMaxLength(200);
        builder.Property(e => e.NumeroIdentificacion).HasMaxLength(50);
        builder.Property(e => e.Salt).HasMaxLength(50);
    }
}
