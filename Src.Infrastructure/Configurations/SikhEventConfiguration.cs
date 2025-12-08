using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Configurations;
public class SikhEventConfiguration : IEntityTypeConfiguration<SikhEvent>
{
    public void Configure(EntityTypeBuilder<SikhEvent> builder)
    {
        builder.HasKey(p => p.SubmissionId);

        builder.Property(s => s.SubmissionId)
            .HasColumnName("submission_id")
            .ValueGeneratedNever();

        builder.Property(s => s.Title)
            .HasColumnName("title");

        builder.Property(s => s.Image)
            .HasColumnName("image");

        builder.Property(s => s.Location)
            .HasColumnName("location");

        builder.Property(s => s.ContactInfo)
            .HasColumnType("contact_info");

        builder.Property(s => s.EventDateTime)
            .HasColumnType("event_date_time");

        builder.HasOne(s => s.Submission)
            .WithOne(s => s.SikhEvent)
            .HasForeignKey<SikhEvent>(s => s.SubmissionId);
    }
}

