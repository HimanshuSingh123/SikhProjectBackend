using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
namespace Src.Infrastructure.Configurations
{
    public class IndividualPermissionsConfiguration : IEntityTypeConfiguration<IndividualPermissions>
    {
        public void Configure(EntityTypeBuilder<IndividualPermissions> builder)
        {
            builder.ToTable("Individual_Permissions");
            builder.HasKey(ip => ip.Permission);
            builder.Property(accountType => accountType.Permission).HasColumnName("permission");
        }
    }
}
