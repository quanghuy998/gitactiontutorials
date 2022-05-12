using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Queries.Tests
{
    public class GetTestDetailsHandlerTest
    {
        private readonly Mock<ITestRepository> mockTestRepository = new Mock<ITestRepository>();
        private GetTestDetailsQuery query = new GetTestDetailsQuery() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenInformation_WhenGettingTestDetails_ThenItShouldReturnTest()
        {
            var test = GivenSampleTest();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new GetTestDetailsQueryHandler(mockTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(test, result);
        }

        [Fact]
        public async Task GivenInformation_WhenGettingDetailsOfTheTestDoesNotExist_ThenItShouldReturnNull()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetTestDetailsQueryHandler(mockTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
