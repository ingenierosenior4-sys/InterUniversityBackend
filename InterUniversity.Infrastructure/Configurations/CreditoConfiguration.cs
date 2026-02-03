using InterUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterUniversity.Infrastructure.Configurations;

internal sealed class CreditoConfiguration : IEntityTypeConfiguration<Credito>
{
    public void Configure(EntityTypeBuilder<Credito> builder)
    {
        builder.HasKey(e => e.CreditoId);

        builder.ToTable("Credito");

        builder.Property(e => e.Descripcion).HasMaxLength(200);
    }
}
