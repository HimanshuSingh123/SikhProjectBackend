using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(p => p.SubmissionId);

        builder.Property(c => c.SubmissionId)
            .HasColumnName("submission_id")
            .ValueGeneratedNever();

        builder.Property(c => c.CourseName)
            .HasColumnName("course_name");

        builder.Property(c => c.Description)
            .HasColumnName("description");

        builder.Property(c => c.Image)
            .HasColumnName("image");

        builder.Property(c => c.UploadedMaterial)
            .HasColumnName("uploaded_material");

        builder.Property(c => c.Price)
            .HasColumnName("price");

        builder.HasOne(c => c.Submission)
            .WithOne(s => s.Course)
            .HasForeignKey<Course>(c => c.SubmissionId);
    }
}

