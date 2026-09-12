using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class MerchConfiguration : IEntityTypeConfiguration<Merch>
{
    public void Configure(EntityTypeBuilder<Merch> builder)
    {
        builder.ToTable("Merch");

        builder.HasKey(p => p.SubmissionId);

        builder.Property(p => p.SubmissionId)
            .HasColumnName("submission_id")
            .ValueGeneratedNever();

        builder.Property(m => m.Title)
            .HasColumnName("title")
            .IsRequired();

        builder.HasIndex(m => m.Title)
            .IsUnique();

        builder.Property(p => p.Description)
            .HasColumnName("description");

        builder.Property(p => p.Image)
            .HasColumnName("image");

        builder.Property(p => p.Size)
            .HasColumnName("size")
            .IsRequired();

        builder.Property(p => p.QuantityMax)
            .HasColumnName("qty_max")
            .IsRequired();

        builder.Property(p => p.QuantityMin)
            .HasColumnName("qty_min")
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(p => p.Rating)
            .HasColumnName("rating");

        builder.HasOne(p => p.Submission)
            .WithOne(m => m.Merch)
            .HasForeignKey<Merch>(m => m.SubmissionId);
    }
}

