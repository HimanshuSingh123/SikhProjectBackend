using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;
public class NewsfeedConfiguration : IEntityTypeConfiguration<Newsfeed>
{
    public void Configure(EntityTypeBuilder<Newsfeed> builder)
    {
        builder.HasKey(p => p.SubmissionId);
    }
}

