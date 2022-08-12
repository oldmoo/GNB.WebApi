using GNB.Application.ApplicationServicesContracts.Transaction;
using GNB.Application.ApplicationServicesContracts.TransactionBySku;
using GNB.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GNB.Api.Controllers;

[ApiController]
[Route("api/transactions/")]
[Produces("application/json")]

public class TransactionController : ControllerBase
{
     private readonly ITransactionAppService _transactionAppService;
     private readonly ITransactionAppBySkuService _transactionAppBySkuService;
     
     public TransactionController(ITransactionAppBySkuService transactionAppBySkuService, ITransactionAppService transactionAppService)
     {
          _transactionAppBySkuService = transactionAppBySkuService;
          _transactionAppService = transactionAppService;
     }

     [HttpGet(Name = "Get list of transactions")]
     [ProducesResponseType(typeof(List<TransactionDto>), StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     public async Task<ActionResult<List<TransactionDto>>> GetTransactions()
     {
          try
          {
               var transactions = await _transactionAppService.Get();
               if (transactions.Any()) return Ok(transactions);
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
               var transactionsBySku = await _transactionAppBySkuService.GetTransactionBySku(sku);
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