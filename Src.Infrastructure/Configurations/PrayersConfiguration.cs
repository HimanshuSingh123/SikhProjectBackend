using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class PrayersConfiguration : IEntityTypeConfiguration<Prayers>
{
    public void Configure(EntityTypeBuilder<Prayers> builder)
    {
        builder.HasKey(p => p.PrayerName);
    }
}

