namespace GNB.Domain.InfrastructureContracts;

public interface IUnitOfWork : IDisposable
{
     IRateRepository RateRepository { get; }
     ITransactionRepository TransactionRepository { get; }
     Task<int> SaveChangesAsync();
}