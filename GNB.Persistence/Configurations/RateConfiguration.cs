using GNB.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNB.Infrastructure.Configurations;

public class RateConfiguration : EntityConfiguration<Rate>
{
     public override void Configure(EntityTypeBuilder<Rate> builder)
     {
         
          _ = builder.Property(r => r.From).IsRequired();
          _ = builder.Property(r => r.To).IsRequired();
          _ = builder.Property(r => r.Value).IsRequired();
          
          base.Configure(builder);
     }
}