using GNB.Domain.Entities;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Contexts;

namespace GNB.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
     public TransactionRepository(AppDbContext context) : base(context)
     {
     }
}