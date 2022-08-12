namespace GNB.Domain.DomainServicesContracts.Transaction;

public interface ITransactionDomainService : IBaseService<Entities.Transaction>
{
     Task AddRangeAsync(IEnumerable<Entities.Transaction> rates);
}