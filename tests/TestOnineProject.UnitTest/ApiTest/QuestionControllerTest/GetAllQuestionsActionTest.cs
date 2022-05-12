using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;

namespace TestOnineProject.UnitTest.ApiTest.QuestionControllerTest
{
    public class GetAllQuestionsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();

        [Fact]
        public async Task GivenRequest_WhenGettingAllQuestion_ThenItShouldReturnOk()
        {
            var expected = GivenSampleQuestionList();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetAllQuestionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetAllQuestions(CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var actual = okResult?.Value as List<Question>;

            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(expected, actual);
        }

        private List<Question> GivenSampleQuestionList()
        {
            return new List<Question>
            {
                new Question("Question test 1", 3, QuestionType.MultipChoice),
                new Question("Question test 2", 4, QuestionType.Code)
            };
        }
    }
}
