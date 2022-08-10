using GNB.Application.Dtos;
using GNB.Domain.Enums;

namespace GNB.Application.ApplicationServicesContracts.TransactionBySku;

public interface ITransactionBySkuService
{
     Task<TransactionBySkuDto?> GetTransactionBySku(string sku, CancellationToken token);
     Task<bool> SkuExists(string sku);
     Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount, CancellationToken token);
     Task<decimal> GetExistingRate(Currency from, Currency to, CancellationToken token);
     Task<decimal> Rate(Currency from, Currency to, CancellationToken token);
     
}