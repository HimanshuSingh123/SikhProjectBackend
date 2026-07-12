using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class TemplesConfiguration : IEntityTypeConfiguration<Temples>
{
    public void Configure(EntityTypeBuilder<Temples> builder)
    {
        builder.ToTable("Temples");

        builder.HasKey(t => t.TempleId);

        builder.Property(t => t.TempleId)
            .HasColumnName("temple_id")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(t => t.Address)
            .HasColumnName("address")
            .IsRequired();

        builder.Property(t => t.lat)
            .HasColumnName("lat");

        builder.Property(t => t.lon)
            .HasColumnName("lon");
    }
}

