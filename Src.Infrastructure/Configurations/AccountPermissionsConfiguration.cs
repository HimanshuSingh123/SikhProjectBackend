using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations
{
    public class AccountPermissionsConfiguration : IEntityTypeConfiguration<AccountPermissions>
    {
        public void Configure(EntityTypeBuilder<AccountPermissions> builder)
        {
            builder.ToTable("Account_Permissions");


            builder.HasKey(a => new { a.AccountTypeName, a.Permission });

            builder.Property(accountType => accountType.Permission).HasColumnName("permission").IsRequired();
            builder.Property(accountType => accountType.AccountTypeName).HasColumnName("account_type").IsRequired();

            builder.HasOne(ap => ap.IndividualPermissions)
                .WithMany(ip => ip.AccountPermissions)
                .HasForeignKey(ip => ip.Permission)
                .HasPrincipalKey(ip => ip.Permission);

            builder.HasOne(ap => ap.AccountType)
                .WithMany(at => at.AccountPermissions)
                .HasForeignKey(ap => ap.AccountTypeName)
                .HasPrincipalKey(ap => ap.AccountTypeName);

        }
    }
}
