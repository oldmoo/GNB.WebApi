using GNB.Application.ApplicationServicesContracts.Transaction;
using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.Entities;

namespace GNB.Application.ApplicationServicesImplementations;

public class TransactionAppService : ITransactionAppService
{
     private readonly ITransactionDomainService _transactionDomainService;

     public TransactionAppService(ITransactionDomainService transactionDomainService)
     {
          _transactionDomainService = transactionDomainService;
     }

     public async Task<IEnumerable<Transaction>> Get()
     {
          return await _transactionDomainService.Get();
     }
}