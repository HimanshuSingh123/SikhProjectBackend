using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");

        builder.HasKey(p => p.CartId);

        builder.Property(c => c.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(c => c.ItemTitle)
            .HasColumnName("item_title")
            .IsRequired();

        builder.Property(c => c.ItemDescription)
            .HasColumnName("item_description");

        builder.Property(c => c.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(c => c.Category)
            .HasColumnName("category")
            .IsRequired();

        builder.Property(c => c.CartId)
            .HasColumnName("card_id")
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.Username)
            .HasPrincipalKey(u => u.Username);


    }
}

