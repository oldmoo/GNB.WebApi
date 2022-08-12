using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts;

namespace GNB.Application.ApplicationServicesContracts.Transaction;

public interface ITransactionAppService
{
     Task<IEnumerable<TransactionDto>> Get();
}