// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using GNB.Api.Controllers;
// using GNB.Application.ApplicationServicesContracts.Transaction;
// using GNB.Application.ApplicationServicesContracts.TransactionBySku;
// using GNB.Application.Dtos;
// using GNB.Application.Extensions;
// using GNB.Domain.DomainServicesContracts.Transaction;
// using GNB.Domain.Entities;
// using GNB.Domain.Enums;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;
//
// namespace GNB.WebApi.UnitTests.ControllerTests;
//
// public class TransactionControllerTest
// {
//      private readonly Mock<ITransactionAppBySkuService> _transactionBySkuServiceMock;
//      private readonly Mock<ITransactionAppService> _transactionServiceMock;
//      private readonly TransactionController _transactionController;
//      private readonly List<Transaction> _transactions;
//      private readonly TransactionBySkuDto _transactionBySkuDto;
//      public TransactionControllerTest()
//      {
//           _transactionBySkuServiceMock = new Mock<ITransactionAppBySkuService>(MockBehavior.Strict);
//           _transactionServiceMock = new Mock<ITransactionAppService>(MockBehavior.Strict);
//           _transactionController = new TransactionController(_transactionBySkuServiceMock.Object, _transactionServiceMock.Object);
//           _transactions = new List<Transaction>
//           {
//                new() { Sku = "F1095", Amount = 20.7m, Currency = Currency.Cad },
//                new() { Sku = "F1095", Amount = 21.14m, Currency = Currency.Aud },
//                new() { Sku = "F1095", Amount = 24.8m, Currency = Currency.Usd },
//                new() { Sku = "F1095", Amount = 34.0m, Currency = Currency.Eur },
//                new() { Sku = "I8437", Amount = 16.3m, Currency = Currency.Aud },
//                new() { Sku = "Q7445", Amount = 32.7m, Currency = Currency.Cad },
//                new() { Sku = "S7785", Amount = 20.3m, Currency = Currency.Aud },
//                new() { Sku = "I5382", Amount = 22.2m, Currency = Currency.Usd },
//                new() { Sku = "E6784", Amount = 20.8m, Currency = Currency.Usd },
//                new() { Sku = "K9868", Amount = 26.3m, Currency = Currency.Eur },
//           };
//
//           var transactions = _transactions.Where(t => t.Sku.Equals("F1095")).ToList();
//           _transactionBySkuDto = new TransactionBySkuDto
//           {
//                TotalAmount = transactions.Sum(t => t.Amount),
//                Transactions = transactions.TransactionToDto().ToList()
//                
//           };
//      }
//      
//      [Fact]
//      private async Task GetTransactions_ShouldReturn_OkObjectResult()
//      {
//           // Arrange
//           _transactionServiceMock.Setup(x => x.Get()).ReturnsAsync(_transactions);
//          
//           // Act
//           var result = await _transactionController.GetTransactions();
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<List<TransactionDto>>>(result);
//           Assert.IsType<OkObjectResult>(actionResult.Result);
//      }
//      
//      [Fact]
//      private async Task GetTransactions_ShouldReturn_NotFoundObjectResult()
//      {
//           // Arrange
//           _transactionServiceMock.Setup(x => x.Get()).ReturnsAsync(Array.Empty<Transaction>());
//          
//           // Act
//           var result = await _transactionController.GetTransactions();
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<List<TransactionDto>>>(result);
//           Assert.IsType<NotFoundResult>(actionResult.Result);
//      }
//      
//      [Fact]
//      private async Task GetTransactions_ShouldReturn_IEnumerableOfTransactionDtoAsModelType()
//      {
//           // Arrange
//           _transactionServiceMock.Setup(x => x.Get()).ReturnsAsync(_transactions);
//          
//           // Act
//           var result = await _transactionController.GetTransactions();
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<List<TransactionDto>>>(result);
//           Assert.IsAssignableFrom<IEnumerable<TransactionDto>>(((ObjectResult)actionResult.Result!).Value);
//      }
//      
//      [Fact]
//      private async Task GetTransactionBySkuInEur_ShouldReturn_OkObjectResult()
//      {
//           // Arrange
//           _transactionBySkuServiceMock.Setup(x => x.GetTransactionBySku(It.IsAny<string>())).ReturnsAsync(_transactionBySkuDto);
//
//
//           // Act
//           var result = await _transactionController.GeTransactionBySku("F1095");
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<TransactionBySkuDto>>(result);
//           Assert.IsType<OkObjectResult>(actionResult.Result);
//      }
//      
//      [Fact]
//      private async Task GetTransactionBySkuInEur_ShouldReturn_BadRequestObjectResult()
//      {
//           // Arrange
//           _transactionBySkuServiceMock.Setup(x => x.GetTransactionBySku(It.IsAny<string>())).ReturnsAsync(_transactionBySkuDto);
//
//           // Act
//           var result = await _transactionController.GeTransactionBySku(string.Empty);
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<TransactionBySkuDto>>(result);
//           Assert.IsType<BadRequestResult>(actionResult.Result);
//      }
//      [Fact]
//      private async Task GetTransactionBySkuInEur_ShouldReturn_NotFoundObjectResult()
//      {
//           // Arrange
//           _transactionBySkuServiceMock.Setup(x => x.GetTransactionBySku(It.IsAny<string>())).ReturnsAsync((TransactionBySkuDto?)null);
//          
//           // Act
//           var result = await _transactionController.GeTransactionBySku("F1095");
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<TransactionBySkuDto>>(result);
//           Assert.IsType<NotFoundResult>(actionResult.Result);
//      }
//      
//      [Fact]
//      private async Task GetTransactionBySkuInEur_ShouldReturn_TransactionSkuDtoAsModelType()
//      {
//           // Arrange
//           _transactionBySkuServiceMock.Setup(x => x.GetTransactionBySku(It.IsAny<string>())).ReturnsAsync(_transactionBySkuDto);
//           
//           // Act
//           var result = await _transactionController.GeTransactionBySku("J6456");
//
//           // Assert
//           var actionResult = Assert.IsType<ActionResult<TransactionBySkuDto>>(result);
//           Assert.IsAssignableFrom<TransactionBySkuDto>(((ObjectResult)actionResult.Result!).Value);
//      }
// }