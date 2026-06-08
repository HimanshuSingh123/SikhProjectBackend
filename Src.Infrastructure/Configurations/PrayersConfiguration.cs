using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class PrayersConfiguration : IEntityTypeConfiguration<Prayers>
{
    public void Configure(EntityTypeBuilder<Prayers> builder)
    {
        builder.ToTable("Prayers");

        builder.HasKey(p => p.PrayerName);

        builder.Property(p => p.PrayerContent)
            .HasColumnName("prayer_content");
    }
}

