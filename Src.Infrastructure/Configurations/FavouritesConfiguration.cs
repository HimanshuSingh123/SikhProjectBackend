using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class FavouritesConfiguration : IEntityTypeConfiguration<Favourites>
{
    public void Configure(EntityTypeBuilder<Favourites> builder)
    {
        builder.ToTable("Favourites");

        builder.HasKey(p => p.FavId);

        builder.HasIndex(f => new {f.Username, f.SubmissionId}, "FavouritesUserSubmissionKey")
            .IsUnique();

        builder.Property(f => f.FavId)
            .HasColumnName("fav_ID")
            .ValueGeneratedOnAdd();

        builder.Property(f => f.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(f => f.ItemTitle)
            .HasColumnName("item_title")
            .IsRequired();

        builder.Property(f => f.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(f => f.ItemDescription)
            .HasColumnName("item_description");

        builder.Property(f => f.Category)
            .HasColumnName("category")
            .IsRequired();

        builder.Property(f => f.SubmissionId)
            .HasColumnName("submission_id")
            .IsRequired();

        builder.HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.Username)
            .HasPrincipalKey(u => u.Username);

        builder.HasOne(favourites => favourites.Submission)
            .WithMany(s => s.Favourites)
            .HasForeignKey(f => f.SubmissionId);
    }
}

