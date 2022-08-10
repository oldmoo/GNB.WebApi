using GNB.Application.Dtos;
using GNB.Domain.Entities;

namespace GNB.Application.Extensions;

public static class EntityToDto
{
     public static IEnumerable<RateDto> RateToDto(this IEnumerable<Rate> rates)
     {
          return rates.Select(r => new RateDto
          {
               From = r.From,
               To = r.To,
               Value = r.Value
          });
     }
     
     public static IEnumerable<TransactionDto> TransactionToDto(this IEnumerable<Transaction> rates)
     {
          return rates.Select(t => new TransactionDto
          {
               Sku = t.Sku,
               Amount = t.Amount,
               Currency = t.Currency
          });
     }
}