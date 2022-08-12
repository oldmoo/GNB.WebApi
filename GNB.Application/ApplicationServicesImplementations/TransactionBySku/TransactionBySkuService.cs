using AutoMapper;
using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.Entities;
using GNB.Domain.Enums;
using GNB.Domain.Helper;
using GNB.Domain.InfrastructureContracts;

namespace GNB.Application.ApplicationServicesImplementations.TransactionBySku;

public class TransactionAppBySkuService : ITransactionAppBySkuService
{
     private readonly IUnitOfWork _unitOfWork;
     private readonly IRateDomainService _rateDomainService;
     private readonly IMapper _mapper;
     public TransactionAppBySkuService(IUnitOfWork unitOfWork, IRateDomainService rateDomainService, IMapper mapper)
     {
          _unitOfWork = unitOfWork;
          _rateDomainService = rateDomainService;
          _mapper = mapper;
     }

     public async Task<TransactionBySkuDto?> GetTransactionBySku(string sku)
     {
          var skuExists =  await _unitOfWork.TransactionRepository.ExistAsync(t => t.Sku.Equals(sku));
          if (skuExists  is not true) return default;
          var transactionsBySku = await _unitOfWork.TransactionRepository.GetTransactionsBySku(sku);
          var transactionBySkuDto = new TransactionBySkuDto
          {
               Transactions = new List<TransactionDto>()
          };
          var transactions = _mapper.Map<List<Transaction>>(transactionBySkuDto.Transactions);
          
          foreach (var trans in transactionsBySku)
          {
               transactionBySkuDto.Transactions.Add(new TransactionDto()
               {
                   Amount = await RoundAmount(trans.Currency, trans.Amount),
                   Currency = Currency.Eur,
                   Sku = trans.Sku
               });
               transactionBySkuDto.TotalAmount = HelperCurrency.RoundTotalAmount(transactions);
          }
          return transactionBySkuDto;
     }

     public async Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount)
     {
          return amount * await _rateDomainService.GetRateByCurrency(currency, currencyInEur);
     }

     private async Task<decimal> RoundAmount(Currency currency, decimal amount)
     {
          return Math.Round((currency != Currency.Eur ? await GetAmountByCurrency(currency, Currency.Eur, amount) : amount), 2);
     }
}