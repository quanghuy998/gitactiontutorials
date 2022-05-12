using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnineProejct.UnitTest.ApiTest.TestControllerTest
{
    public class GetAllTestsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();

        [Fact]
        public async Task GivenRequest_WhenGettingAllQuestion_ThenItShouldReturnOk()
        {
            var expected = GivenSampleTestList();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetAllTestsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetAllTests(CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var actual = okResult?.Value as List<Test>;

            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(expected, actual);
        }

        private List<Test> GivenSampleTestList()
        {
            return new List<Test>
            {
                new Test("Title 1"),
                new Test("Title 2"),
                new Test("Title 3")
            };
        }
    }
}
