using GNB.Domain.Entities;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GNB.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
     public TransactionRepository(AppDbContext context) : base(context)
     {
     }

     public async Task<IEnumerable<Transaction>?> GetTransactionsBySku(string sku)
     {
          return await Context.Transactions.Where(t => t.Sku == sku).ToListAsync();
     }
}