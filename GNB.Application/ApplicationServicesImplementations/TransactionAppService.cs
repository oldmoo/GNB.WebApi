using AutoMapper;
using GNB.Application.ApplicationServicesContracts.Transaction;
using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.Entities;

namespace GNB.Application.ApplicationServicesImplementations;

public class TransactionAppService : ITransactionAppService
{
     private readonly ITransactionDomainService _transactionDomainService;
     private readonly IMapper _mapper;
     public TransactionAppService(ITransactionDomainService transactionDomainService, IMapper mapper)
     {
          _transactionDomainService = transactionDomainService;
          _mapper = mapper;
     }

     public async Task<IEnumerable<TransactionDto>> Get()
     {
          var transactions = await _transactionDomainService.Get();
          var transactionsDto = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
          return transactionsDto;
     }
}