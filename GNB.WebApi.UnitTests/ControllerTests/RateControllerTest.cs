using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GNB.Api.Controllers;
using GNB.Application.ApplicationServicesContracts.Rate;
using GNB.Application.Dtos;
using GNB.Domain.Entities;
using GNB.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GNB.WebApi.UnitTests.ControllerTests;

public class RateControllerTest
{
     private readonly Mock<IRateAppService> _rateServiceMock;
     private readonly RateController _rateController;
     private readonly List<Rate>  _rates;

     public RateControllerTest()
     {
          _rateServiceMock = new Mock<IRateAppService>(MockBehavior.Strict);
          _rateController = new RateController(_rateServiceMock.Object);
          _rates = new List<Rate>
          {
               new() { From = Currency.Usd, To = Currency.Eur, Value = 1.1m},
               new() { From = Currency.Eur, To = Currency.Usd, Value = 0.91m},
               new() { From = Currency.Usd, To = Currency.Aud, Value = 1.38m},
               new() { From = Currency.Aud, To = Currency.Usd, Value = 0.72m},
               new() { From = Currency.Eur, To = Currency.Cad, Value = 1.38m},
               new() { From = Currency.Cad, To = Currency.Eur, Value = 0.72m},
          };
     }
     
     [Fact]
     private async Task GetRates_ShouldReturn_OkObjectResult()
     {
          // Arrange
          _rateServiceMock.Setup(x => x.Get()).ReturnsAsync(_rates);
         
          // Act
          var result = await _rateController.GetRates();

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsType<OkObjectResult>(actionResult.Result);
     }
     
     [Fact]
     private async Task GetRates_ShouldReturn_NotFoundObjectResult()
     {
          // Arrange
          _rateServiceMock.Setup(x => x.Get()).ReturnsAsync(Array.Empty<Rate>());
         
          // Act
          var result = await _rateController.GetRates();

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsType<NotFoundResult>(actionResult.Result);
     }
     
     [Fact]
     private async Task GetRates_ShouldReturn_IEnumerableOfRateDtoAsModelType()
     {
          // Arrange
          _rateServiceMock.Setup(x => x.Get()).ReturnsAsync(_rates);
         
          // Act
          var result = await _rateController.GetRates();

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsAssignableFrom<IEnumerable<RateDto>>(((ObjectResult)actionResult.Result!).Value);
     }
}