using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using Xunit;
using static TestOnlineProject.API.Dtos.QuestionRequest;

namespace TestOnineProject.UnitTest.ApiTest.QuestionControllerTest
{
    public class UpdateQuestionActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private readonly UpdateQuestionRequest request = new UpdateQuestionRequest("Question number 1", QuestionType.MultipChoice, 2);

        [Fact]
        public async Task GivenInformation_WhenUpdatingQuestion_ThenItShouldReturnOk()
        {
            var questionId = Guid.NewGuid();
            var commandResult = CommandResult.Success();
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateQuestionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new QuestionController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateQuestion(questionId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
