using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;
public class NewsfeedConfiguration : IEntityTypeConfiguration<Newsfeed>
{
    public void Configure(EntityTypeBuilder<Newsfeed> builder)
    {
        builder.ToTable("NewsFeed");

        builder.HasKey(p => p.SubmissionId);

        builder.Property(n => n.SubmissionId)
            .HasColumnName("submission_id")
            .ValueGeneratedNever();

        builder.Property(n => n.Title)
            .HasColumnName("title");

        builder.Property(n => n.Description)
            .HasColumnName("description");

        builder.Property(n => n.Image)
            .HasColumnName("image");

        builder.Property(n => n.Alert)
            .HasColumnName("alert");

        builder.HasOne(n => n.Submission)
            .WithOne(s => s.Newsfeed)
            .HasForeignKey<Newsfeed>(n => n.SubmissionId);
    }
}

