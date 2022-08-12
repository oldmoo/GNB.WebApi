using GNB.Application.ApplicationServicesContracts.Rate;
using GNB.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GNB.Api.Controllers;

[ApiController]
[Route("api/rates")]
public class RateController : ControllerBase
{
     private readonly IRateAppService _rateAppService;
     public RateController(IRateAppService rateAppService)
     {
          _rateAppService = rateAppService;
     }
     
     [HttpGet(Name = "Get list of rates")]
     [ProducesResponseType(typeof(List<RateDto>),StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     public async Task<ActionResult<List<RateDto>>> GetRates()
     {
          try
          {
               
               var rates = await _rateAppService.Get();
               if (rates.Any()) return Ok(rates);
               
               return NotFound();
          }
          catch (Exception ex)
          {
               return StatusCode(500, $"Internal server error, exception: {ex.Message}");
          }
     }
}