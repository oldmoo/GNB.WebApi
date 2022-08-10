using GNB.Domain.Enums;

namespace GNB.Application.Dtos;
[Serializable]
public class TransactionDto
{
     public string Sku { get; set; }
     public decimal Amount { get; set; }
     public Currency Currency { get; set; }
}