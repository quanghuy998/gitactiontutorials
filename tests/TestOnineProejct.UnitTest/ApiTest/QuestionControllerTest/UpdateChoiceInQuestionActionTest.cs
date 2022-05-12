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
using static TestOnlineProject.API.Dtos.QuestionRequest;

namespace TestOnineProejct.UnitTest.ApiTest.QuestionControllerTest
{
    public class UpdateChoiceInQuestionActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly UpdateChoiceInQuestionRequest request = new UpdateChoiceInQuestionRequest("Choice 2", true);

        [Fact]
        public async Task GivenInformation_WhenUpdatingChoiceInQuestion_ThenItShouldReturnOk()
        {
            var questionId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateChoiceInQuestionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateChoiceInQuestion(questionId, choiceId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
