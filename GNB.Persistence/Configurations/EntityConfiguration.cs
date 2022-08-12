using GNB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNB.Infrastructure.Configurations;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
     public virtual void Configure(EntityTypeBuilder<T> builder)
     {
          _ = builder.HasKey(e => e.Id);
          _ = builder.Property(m => m.Id).ValueGeneratedOnAdd().IsRequired();
          _ = builder.Property(m => m.DateCreated).ValueGeneratedOnAdd().IsRequired();
          _ = builder.Property(m => m.DateModified).ValueGeneratedOnAdd().IsRequired();
     }
}