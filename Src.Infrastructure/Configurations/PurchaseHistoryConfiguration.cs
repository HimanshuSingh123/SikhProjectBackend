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
    }
}

