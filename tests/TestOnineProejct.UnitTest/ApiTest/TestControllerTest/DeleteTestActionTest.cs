using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnineProejct.UnitTest.ApiTest.TestControllerTest
{
    public class DeleteTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();

        [Fact]
        public async Task GivenInformation_WhenDeletingQuestion_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<DeleteTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.DeleteTest(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
