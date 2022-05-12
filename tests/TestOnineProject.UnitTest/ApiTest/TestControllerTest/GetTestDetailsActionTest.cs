using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnineProject.UnitTest.ApiTest.TestControllerTest
{
    public class GetTestDetailsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();

        [Fact]
        public async Task GivenRequest_WhenGettingAllQuestion_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var expected = GivenSampleTest();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetTestDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetTestDetails(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var actual = okResult?.Value as Test;

            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(expected, actual);
        }
        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
