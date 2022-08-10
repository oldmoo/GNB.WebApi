using GNB.Infrastructure.Models;
using Transaction = GNB.Domain.Entities.Transaction;

namespace GNB.Application.Dtos;

public class TransactionBySkuDto
{
     public decimal TotalAmount { get; set; }
     public List<Transaction> Transactions { get; set; }
}