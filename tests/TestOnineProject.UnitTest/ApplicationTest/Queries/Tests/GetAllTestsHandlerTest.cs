using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;
using Xunit;

namespace TestOnineProject.UnitTest.ApplicationTest.Queries.Tests
{
    public class GetAllTestsHandlerTest
    {
        private readonly Mock<ITestRepository> mockTestRepository = new Mock<ITestRepository>();
        private GetAllTestsQuery query = new GetAllTestsQuery();

        [Fact]
        public async Task GivenRequest_WhenGetingAllTests_ThenItShouldReturnTestList()
        {
            var testList = GivenSampleTestList();
            mockTestRepository.Setup(p => p.FindAllAsync(It.IsAny<BaseSpecification<Test>>(), It.IsAny<CancellationToken>())).ReturnsAsync(testList);
            var handler = new GetAllTestsQueryHandler(mockTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var resultList = result as List<Test>;

            Assert.Equal(testList, resultList);
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
