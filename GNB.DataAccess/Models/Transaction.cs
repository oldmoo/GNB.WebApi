using GNB.Domain.Enums;

namespace GNB.Infrastructure.Models;

public class Transaction
{
     public string? Sku { get; init; }
     public decimal Amount { get; init; }
     public Currency Currency { get; set; }
}