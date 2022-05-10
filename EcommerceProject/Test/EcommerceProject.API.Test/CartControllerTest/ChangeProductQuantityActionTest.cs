using EcommerceProject.API.Controllers;
using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Carts.ChangeProductQuantity;
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
    public class ChangeProductQuantityActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly ChangeProductQuantityRequest request = new ChangeProductQuantityRequest()
        {
            ProductId = 1,
            CartProductId = 1,
            Quantity = 5
        };

        [Fact]
        public async Task GivenACart_WhenChangingProductQuantity_ThenItShouldReturnOk()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(customerId);
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<ChangeProductQuantityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.ChangeProductQuantity(customerId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GivenThatNotFoundCartAndProductIsNotExist_ThenItShoundReturnBadRequest()
        {
            var customerId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Error();
            mockCommandBus.Setup(p => p.SendAsyns(It.IsAny<ChangeProductQuantityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.ChangeProductQuantity(customerId, request, CancellationToken.None);
            var badRequestResult = actionResult as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
