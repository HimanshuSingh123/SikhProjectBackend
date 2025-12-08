using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.HasKey(p => p.SubmissionId);

        builder.Property(s => s.SubmissionId)
            .HasColumnName("submission_id")
            .IsRequired();

        builder.Property(s => s.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(s => s.DateSubmitted)
            .HasColumnName("date_submitted")
            .IsRequired();
        builder.Property(s => s.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(s => s.Category)
            .HasColumnName("category")
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.Username)
            .HasPrincipalKey(u => u.Username)
            .IsRequired();
    }
}

