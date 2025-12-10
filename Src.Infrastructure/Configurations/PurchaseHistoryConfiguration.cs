using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Configurations;

public class PurchaseHistoryConfiguration : IEntityTypeConfiguration<PurchaseHistory>
{
    public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
    {
        builder.HasKey(p => p.TransactionId);

        builder.Property(p => p.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(p => p.ItemTitle)
            .HasColumnName("item_title")
            .IsRequired();

        builder.Property(p => p.price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(p => p.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(p => p.ItemType)
            .HasColumnName("item_type");

        builder.Property(p => p.PurchaseTimestamp)
            .HasColumnName("purchase_timestamp");

        builder.HasOne(p => p.User)
            .WithMany(u => u.PurchaseHistories)
            .HasForeignKey(p => p.Username)
            .HasPrincipalKey(u => u.Username);
            
    }
}

