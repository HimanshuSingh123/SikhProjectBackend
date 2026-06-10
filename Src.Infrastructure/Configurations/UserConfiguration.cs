using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(p => p.UserId);

        builder.Property(u => u.Email).HasColumnName("email").IsRequired();

        builder.Property(u => u.Username).HasColumnName("username").IsRequired();

        builder.Property(u => u.HashedPass).HasColumnName("hashed_pass").IsRequired();

        builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();

        builder.Property(u => u.AccountTypeName).HasColumnName("account_type").IsRequired();

        builder.HasOne(u => u.AccountType)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AccountTypeName);

    }
}

