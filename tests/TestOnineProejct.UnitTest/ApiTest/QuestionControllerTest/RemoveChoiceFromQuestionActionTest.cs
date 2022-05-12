using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;

namespace TestOnineProejct.UnitTest.ApiTest.QuestionControllerTest
{
    public class RemoveChoiceFromQuestionActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        
        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromQuestion_ThenItShouldReturnOk()
        {
            var questionId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<RemoveChoiceFromQuestionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.RemoveChoiceFromQuestion(questionId, choiceId, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
