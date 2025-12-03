using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class MerchConfiguration : IEntityTypeConfiguration<Merch>
{
    public void Configure(EntityTypeBuilder<Merch> builder)
    {
        builder.HasKey(p => p.SubmissionId);
    }
}

