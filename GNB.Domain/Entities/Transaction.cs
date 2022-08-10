using GNB.Domain.Enums;

namespace GNB.Domain.Entities;

public class Transaction : BaseEntity
{
     public string Sku { get; set; }
     public decimal Amount { get; set; }
     public Currency Currency { get; set; }
}