using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.Dtos;
using GNB.Application.Helper;
using GNB.Domain.Enums;
using GNB.Domain.InfrastructureContracts;
using Transaction = GNB.Domain.Entities.Transaction;

namespace GNB.Application.ApplicationServicesImplementations.TransactionBySku;

public class TransactionBySku : ITransactionBySkuService
{
     private readonly IUnitOfWork _unitOfWork;
     
     public TransactionBySku(IUnitOfWork unitOfWork)
     {
          _unitOfWork = unitOfWork;
     }

     public async Task<TransactionBySkuDto?> GetTransactionBySku(string sku, CancellationToken token)
     {
          if (await SkuExists(sku) is not true) return default;
          var transactions = await _unitOfWork.TransactionRepository.ListAllAsync(token);
          var transactionBySku = new TransactionBySkuDto
          {
               Transactions = new List<Transaction>()
          };

          foreach (var trans in transactions.Where(t => t.Sku.Equals(sku)))
          {
               transactionBySku.Transactions.Add(new Transaction
               {
                   Amount = await RoundAmount(trans.Currency, trans.Amount, token),
                   Currency = Currency.Eur,
                   Sku = trans.Sku
               });
               transactionBySku.TotalAmount = HelperCurrency.RoundTotalAmount(transactionBySku.Transactions);
          }
          return transactionBySku;
     }

     public async Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount, CancellationToken token)
     {
          return amount * await Rate(currency, currencyInEur, token);
     }
     
     public async Task<bool> SkuExists(string sku)
     {
          return await _unitOfWork.TransactionRepository.ExistAsync(t => t.Sku.Equals(sku));
     }
     
     public async Task<decimal> GetExistingRate(Currency from, Currency to, CancellationToken token)
     {
          var rate = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == from && r.To == to, token);
          var rateInverse = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == to && r.To == from, token);
         
          var rateValue = rate?.Value ?? 1 / rateInverse!.Value;
          return rateValue;
     }
     public async Task<decimal> Rate(Currency from, Currency to, CancellationToken token)
     {
          var currDictionary =
               HelperCurrency.GetCurrencyDictionary(await _unitOfWork.RateRepository.ListAllAsync(token));
          
          if (currDictionary is not null && currDictionary[from].Contains(to))
          {
               return await GetExistingRate(from, to, token);
          }

          foreach (var code in currDictionary?[from]!)
          {
               var rate = await Rate(code, to, token);
               if (rate != 0)
               {
                    return rate * await GetExistingRate(from, code, token);
               }
          }
          return 0;
     }
   
     private async Task<decimal> RoundAmount(Currency currency, decimal amount, CancellationToken token)
     {
          return Math.Round((currency != Currency.Eur ? await GetAmountByCurrency(currency, Currency.Eur, amount, token) : amount), 2);
     }
}