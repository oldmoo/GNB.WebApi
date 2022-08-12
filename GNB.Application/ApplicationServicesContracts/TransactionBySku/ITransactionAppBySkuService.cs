using GNB.Application.Dtos;
using GNB.Domain.Enums;

namespace GNB.Application.ApplicationServicesContracts.TransactionBySku;

public interface ITransactionAppBySkuService
{
     Task<TransactionBySkuDto?> GetTransactionBySku(string sku);
     Task<decimal> GetAmountByCurrency(Currency currency, Currency currencyInEur, decimal amount);
}