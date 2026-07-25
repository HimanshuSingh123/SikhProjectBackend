using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;
public class SikhEventConfiguration : IEntityTypeConfiguration<SikhEvent>
{
    public void Configure(EntityTypeBuilder<SikhEvent> builder)
    {
        builder.ToTable("Sikh_Event");

        builder.HasKey(p => p.SubmissionId);

        builder.Property(s => s.SubmissionId)
            .HasColumnName("submission_id")
            .ValueGeneratedNever();

        builder.Property(s => s.Title)
            .HasColumnName("title");

        builder.Property(s => s.Description)
            .HasColumnName("description");

        builder.Property(s => s.Image)
            .HasColumnName("image");

        builder.Property(s => s.lat)
            .HasColumnName("lat");

        builder.Property(s => s.lon)
            .HasColumnName("lon");

        builder.Property(s => s.Location)
            .HasColumnName("location");

        builder.Property(s => s.ContactInfo)
            .HasColumnName("contact_info");

        builder.Property(s => s.EventDateTime)
            .HasColumnName("event_date_time");

        builder.HasOne(s => s.Submission)
            .WithOne(s => s.SikhEvent)
            .HasForeignKey<SikhEvent>(s => s.SubmissionId);
    }
}

