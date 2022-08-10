using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.InfrastructureContracts;
using Microsoft.Extensions.Configuration;

namespace GNB.Domain.DomainServicesImplementations.Transaction;

public class TransactionService : ITransactionService
{
     private readonly IService<Entities.Transaction> _transactionServiceExternal;
     private readonly IUnitOfWork _unitOfWork;

     public TransactionService(IService<Entities.Transaction> transactionServiceExternal, IUnitOfWork unitOfWork, IConfiguration configuration)
     {
          _transactionServiceExternal = transactionServiceExternal;
          _unitOfWork = unitOfWork;
          _transactionServiceExternal.ClientName = configuration["TransactionClientName"];
     }

     public async Task<IEnumerable<Entities.Transaction>> Get(CancellationToken token, bool fromDb = false)
     {
          var transactions = await _transactionServiceExternal.Get(token);
          if (fromDb is not true) await AddRangeAsync(transactions, token);
          
          return fromDb ? await _unitOfWork.TransactionRepository.ListAllAsync(token) : transactions;
     }

     public async Task AddRangeAsync(IEnumerable<Entities.Transaction> rates, CancellationToken token)
     {
        await _unitOfWork.TransactionRepository.AddRangeAsync(rates, token);
        await _unitOfWork.SaveChangesAsync();
     }
}