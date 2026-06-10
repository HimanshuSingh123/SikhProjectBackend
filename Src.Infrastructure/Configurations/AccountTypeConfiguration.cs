using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;

namespace Src.Infrastructure.Configurations
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("Account_Type");

            builder.HasKey(accountType => accountType.AccountTypeName);

            builder.Property(accountType => accountType.AccountTypeName).HasColumnName("account_type").IsRequired();
                
        }
    }
}
