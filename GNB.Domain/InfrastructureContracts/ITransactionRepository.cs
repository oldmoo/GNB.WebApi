using GNB.Domain.Entities;

namespace GNB.Domain.InfrastructureContracts;

public interface ITransactionRepository : IRepository<Transaction>
{
     Task<IEnumerable<Transaction>?> GetTransactionsBySku(string sku);
}