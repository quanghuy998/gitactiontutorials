using EcommerceProject.API.Controllers;
using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Carts.AddProductToCart;
using EcommerceProject.Application.Commands.Carts.RemoveProductFromCart;
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
    public class RemoveProductFromCartActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly RemoveProductFromCartRequest request = new RemoveProductFromCartRequest()
        {
            CartProductId = 1
        };

        [Fact]
        public async Task GivenACart_WhenRemovingProductFromCart_ThenItShouldReturnOk()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(customerId);
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<RemoveProductFromCartCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.RemoveProductFromCart(customerId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GivenThatNoCartFoundByUserId_WhenRemovingProductFromCart_ThenItShouldReturnBadRequest()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Error();
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<RemoveProductFromCartCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.RemoveProductFromCart(customerId, request, CancellationToken.None);
            var badRequestResult = actionResult as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
