using GNB.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNB.Infrastructure.Configurations;

public class TransactionConfiguration : EntityConfiguration<Transaction>
{
     public override void Configure(EntityTypeBuilder<Transaction> builder)
     {
          
          _ = builder.Property(t => t.Sku).IsRequired();
          _ = builder.Property(t => t.Amount).IsRequired();
          _ = builder.Property(t => t.Currency).IsRequired();
          
          base.Configure(builder);
     }
}