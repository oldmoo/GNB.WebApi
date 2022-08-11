using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.Dtos;
using GNB.Application.Extensions;
using GNB.Domain.DomainServicesContracts.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace GNB.Api.Controllers;

[ApiController]
[Route("api/transactions/")]
[Produces("application/json")]

public class TransactionController : ControllerBase
{
     private readonly ITransactionService _transactionService;
     private readonly ITransactionBySkuService _transactionBySkuService;
     
     public TransactionController(ITransactionService transactionService, ITransactionBySkuService transactionBySkuService)
     {
          _transactionService = transactionService;
          _transactionBySkuService = transactionBySkuService;
     }

     [HttpGet(Name = "Get list of transactions")]
     [ProducesResponseType(typeof(List<TransactionDto>), StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     public async Task<ActionResult<List<TransactionDto>>> GetTransactions()
     {
          try
          {
               var transactions = await _transactionService.Get();
               if (transactions.Any()) return Ok(transactions.TransactionToDto());
               return NotFound();
          }
          catch (Exception ex)
          {
               return StatusCode(500, $"Internal server error, exception: {ex.Message}");
          }
     }
     
     [HttpGet("{sku}", Name = "GeTransactionBySku")]
     [ProducesResponseType(typeof(TransactionBySkuDto), StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     public async Task<ActionResult<TransactionBySkuDto>> GeTransactionBySku([FromRoute] string sku)
     {
          try
          {
               var transactionsBySku = await _transactionBySkuService.GetTransactionBySku(sku);
               if (transactionsBySku == null) return NotFound();
               if (!string.IsNullOrWhiteSpace(sku) && sku.Length == 5) return Ok(transactionsBySku);
               return BadRequest();
     
          }
          catch (Exception ex)
          {
               return StatusCode(500, $"Internal server error, exception: {ex.Message}");
          }
     }
}