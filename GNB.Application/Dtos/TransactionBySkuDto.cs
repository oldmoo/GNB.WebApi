namespace GNB.Application.Dtos;

public class TransactionBySkuDto
{
     public decimal TotalAmount { get; set; }
     public List<TransactionDto> Transactions { get; set; }
}