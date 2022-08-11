using GNB.Application.Dtos;
using GNB.Domain.Enums;

namespace GNB.Application.ApplicationServicesContracts.TransactionBySku;

public interface ITransactionBySkuService
{
     Task<TransactionBySkuDto?> GetTransactionBySku(string sku);
     Task<bool> SkuExists(string sku);
     Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount);
     Task<decimal> GetExistingRate(Currency from, Currency to);
     Task<decimal> Rate(Currency from, Currency to);
     
}