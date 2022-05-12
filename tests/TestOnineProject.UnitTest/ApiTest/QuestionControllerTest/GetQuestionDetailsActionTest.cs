using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
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
    public class GetQuestionDetailsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();

        [Fact]
        public async Task GivenRequest_WhenGettingQuestionDetails_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var question = GivenSampleQuestion();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetQuestionDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetQuestionDetails(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var questionRepsonse = okResult?.Value as Question;

            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(question, questionRepsonse);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;
            return new Question(questionText, point, QuestionType.MultipChoice);
        }
    }
}
