using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class ReviewsConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");

        builder.HasKey(r => r.ReviewId);
        
        builder.Property(r => r.ReviewId)
            .HasColumnName("review_id")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.SubmissionId)
            .HasColumnName("submission_id")
            .IsRequired();

        builder.Property(r => r.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(r => r.ModifiedAt)
            .HasColumnName("modified_at");

        builder.Property(r => r.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(r => r.Role)
            .HasColumnName("role")
            .IsRequired();

        builder.Property(r => r.NumericalRating)
            .HasColumnName("numerical_rating");

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(u => u.Username)
            .HasPrincipalKey(u => u.Username);

        builder.HasOne(r => r.Submission)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.SubmissionId);
    



    }
}

