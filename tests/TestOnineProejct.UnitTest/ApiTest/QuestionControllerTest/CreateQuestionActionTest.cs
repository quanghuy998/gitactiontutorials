using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;
using static TestOnlineProject.API.Dtos.QuestionRequest;

namespace TestOnineProejct.UnitTest.ApiTest.QuestionControllerTest
{
    public class CreateQuestionActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly CreateQuestionRequest request = new CreateQuestionRequest("Question number 1", QuestionType.MultipChoice, 4);

        [Fact]
        public async Task GivenInformation_WhenCreatingQuestion_ThenItShouldReturnOk()
        {
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<CreateQuestionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.CreateQuestion(request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
