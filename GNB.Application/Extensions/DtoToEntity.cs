using GNB.Application.Dtos;
using GNB.Domain.Entities;

namespace GNB.Application.Extensions;

public static class DtoToEntity
{

     
     public static IEnumerable<Transaction> TransactionToEntity(this IEnumerable<TransactionDto> rates)
     {
          return rates.Select(t => new Transaction
          {
               Sku = t.Sku,
               Amount = t.Amount,
               Currency = t.Currency
          });
     }
}