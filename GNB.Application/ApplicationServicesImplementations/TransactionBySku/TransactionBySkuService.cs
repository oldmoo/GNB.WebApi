using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.Dtos;
using GNB.Application.Helper;
using GNB.Domain.Enums;
using GNB.Domain.InfrastructureContracts;

namespace GNB.Application.ApplicationServicesImplementations.TransactionBySku;

public class TransactionBySku : ITransactionBySkuService
{
     private readonly IUnitOfWork _unitOfWork;
     
     public TransactionBySku(IUnitOfWork unitOfWork)
     {
          _unitOfWork = unitOfWork;
     }

     public async Task<TransactionBySkuDto?> GetTransactionBySku(string sku)
     {
          if (await SkuExists(sku) is not true) return default;
          var transactionsBySku = await _unitOfWork.TransactionRepository.GetTransactionsBySku(sku);
          var transactionBySkuDto = new TransactionBySkuDto
          {
               Transactions = new List<TransactionDto>()
          };

          foreach (var trans in transactionsBySku)
          {
               transactionBySkuDto.Transactions.Add(new TransactionDto()
               {
                   Amount = await RoundAmount(trans.Currency, trans.Amount),
                   Currency = Currency.Eur,
                   Sku = trans.Sku
               });
               transactionBySkuDto.TotalAmount = HelperCurrency.RoundTotalAmount(transactionBySkuDto.Transactions);
          }
          return transactionBySkuDto;
     }

     public async Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount)
     {
          return amount * await Rate(currency, currencyInEur);
     }
     
     public async Task<bool> SkuExists(string sku)
     {
          return await _unitOfWork.TransactionRepository.ExistAsync(t => t.Sku.Equals(sku));
     }
     
     public async Task<decimal> GetExistingRate(Currency from, Currency to)
     {
          var rate = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == from && r.To == to);
          var rateInverse = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == to && r.To == from);
         
          var rateValue = rate?.Value ?? 1 / rateInverse!.Value;
          return rateValue;
     }
     public async Task<decimal> Rate(Currency from, Currency to)
     {
          var currDictionary =
               HelperCurrency.GetCurrencyDictionary(await _unitOfWork.RateRepository.ListAllAsync());
          
          if (currDictionary is not null && currDictionary[from].Contains(to))
          {
               return await GetExistingRate(from, to);
          }

          foreach (var code in currDictionary?[from]!)
          {
               var rate = await Rate(code, to);
               if (rate != 0)
               {
                    return rate * await GetExistingRate(from, code);
               }
          }
          return 0;
     }
   
     private async Task<decimal> RoundAmount(Currency currency, decimal amount)
     {
          return Math.Round((currency != Currency.Eur ? await GetAmountByCurrency(currency, Currency.Eur, amount) : amount), 2);
     }
}