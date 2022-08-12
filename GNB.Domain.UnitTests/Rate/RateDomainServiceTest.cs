using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.DomainServicesImplementations.Rate;
using GNB.Domain.Enums;
using GNB.Domain.InfrastructureContracts;
using Moq;
using Xunit;

namespace GNB.Domain.UnitTests.Rate;

public class RateDomainServiceTest
{
     private readonly Mock<IService<Entities.Rate>> _rateServiceMock;
     private readonly Mock<IUnitOfWork> _unitOfWorkMock;
     private readonly Mock<IConfiguration> _configurationMock;
     private readonly List<Entities.Rate>  _rates;
     
     public RateDomainServiceTest()
     {
         _rateServiceMock = new Mock<IService<Entities.Rate>>(MockBehavior.Strict).SetupAllProperties();
         _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
         _configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
         
         _rates = new List<Entities.Rate>
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
     public async void GetRate_Should_Return_List_Of_Rate_Dto()
     {
          // Arrange
          SetUpPropertyRateService(_rateServiceMock.Object);
          _configurationMock.Setup(x => x["HttpClient:Rate"]).Returns("RateService");
         
          _rateServiceMock.Setup(x => x.Get()).ReturnsAsync(_rates);
          _rateServiceMock.Setup(x => x.ClientName);
         
          _unitOfWorkMock.Setup(x => x.RateRepository.AddRangeAsync(_rates)).Returns(Task.CompletedTask);
         _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(It.IsAny<int>());
         
         var rateService = CreateRateService();
         
         // Act
         var result = await rateService.Get();
         var rateResult = result.ToList();

         // Assert
         Assert.NotNull(result);
         Assert.Equal(6, rateResult.Count);
     }

     private void SetUpPropertyRateService(IService<Entities.Rate> rateService)
     {
          rateService.ClientName = "RateService";
     }
     private IRateDomainService CreateRateService()
     {
          return new RateDomainDomainService(_rateServiceMock.Object, _unitOfWorkMock.Object, _configurationMock.Object);
     }
}
