using System.Reflection;
using GNB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GNB.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
     public AppDbContext()
     {
     }
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     {
          
     }
     
     public DbSet<Rate?> Rates { get; set; } = null!;
     public DbSet<Transaction> Transactions { get; set; } = null!;

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          base.OnModelCreating(modelBuilder);
     }
}