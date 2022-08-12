using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.DomainServicesImplementations.Transaction;
using GNB.Domain.Enums;
using GNB.Domain.InfrastructureContracts;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace GNB.Domain.UnitTests.Transaction;

public class TransactionDomainServiceTest
{
     private readonly Mock<IService<Entities.Transaction>> _transactionServiceMock;
     private readonly Mock<IUnitOfWork> _unitOfWorkMock;
     private readonly Mock<IConfiguration> _configurationMock;
     private readonly List<Entities.Transaction>  _transactions;

     public TransactionDomainServiceTest()
     {
          _transactionServiceMock = new Mock<IService<Entities.Transaction>>(MockBehavior.Strict).SetupAllProperties();
          _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
          _configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);

          _transactions = new List<Entities.Transaction>
          {
               new() { Amount = 15.25m, Currency = Currency.Aud, Sku = "F4493" },
               new() { Amount = 32.9m, Currency = Currency.Eur, Sku = "J6456" },
               new() { Amount = 18.0m, Currency = Currency.Eur, Sku = "E3878" },
               new() { Amount = 17.0m, Currency = Currency.Aud, Sku = "V0517" },
               new() { Amount = 21.27m, Currency = Currency.Usd, Sku = "J6456" },
               new() { Amount = 15.5m, Currency = Currency.Cad, Sku = "J6456" }
          };
     }

     [Fact]
     public async void GetRate_Should_Return_List_Of_Rate_Dto()
     {
          // Arrange
          SetUpPropertyTransactionService(_transactionServiceMock.Object);
          _transactionServiceMock.Setup(x => x.Get()).ReturnsAsync(_transactions);
         
          _configurationMock.Setup(x => x["HttpClient:Transaction"]).Returns("TransactionService");
          
          _transactionServiceMock.Setup(x => x.Get()).ReturnsAsync(_transactions);
          _transactionServiceMock.Setup(x => x.ClientName);

          _unitOfWorkMock.Setup(x => x.TransactionRepository.AddRangeAsync(_transactions)).Returns(Task.CompletedTask);
          _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(It.IsAny<int>());
          
          var transactionService = CreateTransactionService();
          
          // Act
          var result = await transactionService.Get();
          var transactionsResult = result.ToList();
          
          // Assert
          Assert.NotNull(result);
          Assert.Equal(6, transactionsResult.Count);
         
     }
     
     private void SetUpPropertyTransactionService(IService<Entities.Transaction> transactionService)
     {
          transactionService.ClientName = "TransactionService";
     }
    
     private ITransactionDomainService CreateTransactionService()
     {
          return new TransactionDomainService(_transactionServiceMock.Object, _unitOfWorkMock.Object, _configurationMock.Object);
     }
}