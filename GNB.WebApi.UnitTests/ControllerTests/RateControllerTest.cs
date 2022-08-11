using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GNB.Api.Controllers;
using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.Entities;
using GNB.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GNB.WebApi.UnitTests.ControllerTests;

public class RateControllerTest
{
     private readonly Mock<IRateService> _rateServiceMock;
     private readonly RateController _rateController;
     private readonly List<Rate>  _rateDto;

     public RateControllerTest()
     {
          _rateServiceMock = new Mock<IRateService>(MockBehavior.Strict);
          _rateController = new RateController(_rateServiceMock.Object);
          _rateDto = new List<Rate>
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
          var token = new CancellationTokenSource().Token;
          _rateServiceMock.Setup(x => x.Get(token)).ReturnsAsync(_rateDto);
         
          // Act
          var result = await _rateController.GetRates(token);

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsType<OkObjectResult>(actionResult.Result);
     }
     
     [Fact]
     private async Task GetRates_ShouldReturn_NotFoundObjectResult()
     {
          // Arrange
          var token = new CancellationTokenSource().Token;
          _rateServiceMock.Setup(x => x.Get(token)).ReturnsAsync(Array.Empty<Rate>());
         
          // Act
          var result = await _rateController.GetRates(token);

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsType<NotFoundResult>(actionResult.Result);
     }
     
     [Fact]
     private async Task GetRates_ShouldReturn_IEnumerableOfRateDtoAsModelType()
     {
          // Arrange
          var token = new CancellationTokenSource().Token;
          _rateServiceMock.Setup(x => x.Get(token)).ReturnsAsync(_rateDto);
         
          // Act
          var result = await _rateController.GetRates(token);

          // Assert
          var actionResult = Assert.IsType<ActionResult<List<RateDto>>>(result);
          Assert.IsAssignableFrom<IEnumerable<RateDto>>(((ObjectResult)actionResult.Result!).Value);
     }
}