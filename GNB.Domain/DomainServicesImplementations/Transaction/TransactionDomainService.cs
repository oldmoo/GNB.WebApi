using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.InfrastructureContracts;
using Microsoft.Extensions.Configuration;

namespace GNB.Domain.DomainServicesImplementations.Transaction;

public class TransactionDomainService : ITransactionDomainService
{
     private readonly IService<Entities.Transaction> _transactionServiceExternal;
     private readonly IUnitOfWork _unitOfWork;
     public TransactionDomainService(IService<Entities.Transaction> transactionServiceExternal, IUnitOfWork unitOfWork, IConfiguration configuration)
     {
          _transactionServiceExternal = transactionServiceExternal;
          _unitOfWork = unitOfWork;
          _transactionServiceExternal.ClientName = configuration["HttpClient:Transaction"];
     }

     public async Task<IEnumerable<Entities.Transaction>> Get()
     {
          var transactions = await _transactionServiceExternal.Get();
          await AddRangeAsync(transactions);
          return transactions;
     }

     public async Task AddRangeAsync(IEnumerable<Entities.Transaction> rates)
     {
        await _unitOfWork.TransactionRepository.AddRangeAsync(rates);
        await _unitOfWork.SaveChangesAsync();
     }
}