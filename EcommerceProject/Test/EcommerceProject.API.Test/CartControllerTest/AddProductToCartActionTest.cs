using EcommerceProject.API.Controllers;
using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Carts.AddProductToCart;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.API.Test.CartControllerTest
{
    public class AddProductToCartActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private AddProductToCartRequest request = new AddProductToCartRequest()
        {
            ProductId = 1,
            Quantity = 3
        };

        [Fact]
        public async Task GivenACart_WhenAddingProductToCart_ThenItShoudReturnOk()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(customerId);
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<AddProductToCartCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.AddProductToCart(customerId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GivenThatNoCartFoundAndProductIsNotExist_WhenAddingProductToCart_ThenItShouldReturnBadRequest()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Error();
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<AddProductToCartCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.AddProductToCart(customerId, request, CancellationToken.None);
            var badRequestResult = actionResult as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
