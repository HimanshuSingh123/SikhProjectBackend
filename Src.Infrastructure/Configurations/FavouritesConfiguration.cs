using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class FavouritesConfiguration : IEntityTypeConfiguration<Favourites>
{
    public void Configure(EntityTypeBuilder<Favourites> builder)
    {
        builder.HasKey(p => p.FavId);

        builder.Property(f => f.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(f => f.ItemTitle)
            .HasColumnName("item_title")
            .IsRequired();

        builder.Property(f => f.Price)
            .HasColumnName("price").IsRequired();

        builder.Property(f => f.ItemDescription).HasColumnName("item_description");

        builder.Property(f => f.Category)
            .HasColumnName("category");

        builder.Property(f => f.FavId)
            .HasColumnName("fav_ID")
            .IsRequired();

        builder.HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.Username)
            .HasPrincipalKey(u => u.Username);
    }
}

