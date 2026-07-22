using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class RegisteredCoursesConfiguration : IEntityTypeConfiguration<RegisteredCourses>
{
    public void Configure(EntityTypeBuilder<RegisteredCourses> builder)
    {
        builder.ToTable("Registered_Courses");

        builder.HasKey(rc => new { rc.SubmissionId, rc.Username });

        builder.Property(rc => rc.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(rc => rc.SubmissionId)
            .HasColumnName("submission_id")
            .IsRequired();

        builder.Property(rc => rc.RegisteredAt)
            .HasColumnName("registered_at");

        builder.HasOne(rc => rc.User)
            .WithMany(u => u.RegisteredCourses)
            .HasForeignKey(rc => rc.Username)
            .HasPrincipalKey(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rc => rc.Course)
            .WithMany(c => c.RegisteredCourses)
            .HasForeignKey(rc => rc.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);

    }   
}

