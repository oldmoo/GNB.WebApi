namespace GNB.Domain.DomainServicesContracts.Transaction;

public interface ITransactionService : IBaseService<Entities.Transaction>
{
     Task AddRangeAsync(IEnumerable<Entities.Transaction> rates, CancellationToken token);
}