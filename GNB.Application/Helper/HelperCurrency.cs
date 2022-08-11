using GNB.Application.Dtos;
using GNB.Domain.Entities;
using GNB.Domain.Enums;

namespace GNB.Application.Helper;

public static class HelperCurrency
{
     private static  Dictionary<Currency, List<Currency>>? _currencyDictionary;

 
     public static Dictionary<Currency, List<Currency>>? GetCurrencyDictionary(IEnumerable<Rate> rates)
     {
          if (_currencyDictionary is not null) return _currencyDictionary;
          
          _currencyDictionary = new Dictionary<Currency, List<Currency>>();
          foreach (var rate in rates)
          {
               if (!_currencyDictionary.ContainsKey(rate.From))
                    _currencyDictionary[rate.From] = new List<Currency>();
               if (!_currencyDictionary.ContainsKey(rate.To))
                    _currencyDictionary[rate.To] = new List<Currency>();
               _currencyDictionary[rate.From].Add(rate.To);
               _currencyDictionary[rate.To].Add(rate.From);
          }
          return _currencyDictionary;
     }
     
     public static decimal RoundTotalAmount(IEnumerable<TransactionDto> transactions)
     {
          return Math.Round(transactions.Sum(t => t.Amount), 2);
     }
}