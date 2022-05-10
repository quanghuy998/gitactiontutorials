using EcommerceProject.API.Controllers;
using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Carts.AddProductToCart;
using EcommerceProject.Application.Commands.Carts.PlaceOrder;
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
    public class PlaceOrderActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly PlaceOrderRequest request = new PlaceOrderRequest()
        {
            ShippingAddress = "01 Nguyen Huu Tho, Da Nang.",
            ShippingPhoneNumber = "0000-000-000",
        };

        [Fact]
        public async Task GivenAnOrder_WhenPlacingOrder_ThenItShouldReturnOk()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<PlaceOrderCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.PlaceOrder(customerId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GivenThatNoCartWasFound_WhenPlacingOrder_ThenItShouldReturnBadRequest()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult.Error();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<PlaceOrderCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.PlaceOrder(customerId, request, CancellationToken.None);
            var badRequestResult = actionResult as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
