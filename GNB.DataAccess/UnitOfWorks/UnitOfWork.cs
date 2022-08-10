using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Contexts;

namespace GNB.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IUnitOfWork
{
     private readonly AppDbContext _context;
     public IRateRepository RateRepository { get; }
     public ITransactionRepository TransactionRepository { get; }
     private bool  _disposed;
     
     public UnitOfWork(IRateRepository rateRepository, AppDbContext context, ITransactionRepository transactionRepository)
     {
          RateRepository = rateRepository;
          _context = context;
          TransactionRepository = transactionRepository;
     }
     
     public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
     
     public void Dispose() =>  Dispose(true);

     private void Dispose(bool disposing)
     {
          if (_disposed) return;
          if (disposing)
          {
               _context.Dispose();
          }
          _disposed = true;
     }
}