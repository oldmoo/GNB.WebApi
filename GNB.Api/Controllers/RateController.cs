using GNB.Application.Dtos;
using GNB.Application.Extensions;
using GNB.Domain.DomainServicesContracts.Rate;
using Microsoft.AspNetCore.Mvc;

namespace GNB.Api.Controllers;

[ApiController]
[Route("api/rates")]
public class RateController : ControllerBase
{
     private readonly IRateService _rateService;

     public RateController(IRateService rateService)
     {
          _rateService = rateService;
     }
     
     [HttpGet(Name = "Get list of rates")]
     [ProducesResponseType(typeof(List<RateDto>),StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     public async Task<ActionResult<List<RateDto>>> GetRates()
     {
          try
          {
               var rates = await _rateService.Get();
               if (rates.Any()) return Ok(rates.RateToDto());
               
               return NotFound();
          }
          catch (Exception ex)
          {
               return StatusCode(500, $"Internal server error, exception: {ex.Message}");
          }
     }
}