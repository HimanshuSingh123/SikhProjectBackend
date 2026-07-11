using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class PurchaseHistoryConfiguration : IEntityTypeConfiguration<PurchaseHistory>
{
    public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
    {
        builder.ToTable("Purchase_History");

        builder.HasKey(p => p.TransactionId);

        builder.Property(p => p.TransactionId)
            .HasColumnName("transaction_id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(p => p.ItemTitle)
            .HasColumnName("item_title")
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(p => p.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(p => p.ItemType)
            .HasColumnName("item_type")
            .IsRequired();

        builder.Property(p => p.PurchaseTimestamp)
            .HasColumnName("purchase_timestamp");

        //Submission Id here is nullable because submissions can be deleted over time can be deleted, look at relationships down below (deletebehaviour)
        builder.Property(p => p.SubmissionId)
            .HasColumnName("submission_id");
            

        builder.HasOne(p => p.User)
            .WithMany(u => u.PurchaseHistories)
            .HasForeignKey(p => p.Username)
            .HasPrincipalKey(u => u.Username);

        builder.HasOne(p => p.Submission)
            .WithMany(m => m.PurchaseHistory)
            .HasForeignKey(p => p.SubmissionId)
            .OnDelete(DeleteBehavior.SetNull);
            
    }
}

