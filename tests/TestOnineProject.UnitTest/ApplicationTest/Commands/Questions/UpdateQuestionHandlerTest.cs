using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.ApplicationTest.Commands.Questions
{
    public class UpdateQuestionHandlerTest
    {
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private UpdateQuestionCommand command = new UpdateQuestionCommand()
        {
            QuestionId = Guid.NewGuid(),
            Point = 3,
            QuestionText = "This is question update.",
            QuestionType = QuestionType.Code
        };

        [Fact]
        public async Task GivenInformation_WhenUpdatingAnQuestion_ThenItShouldReturnSuccess()
        {
            var question = GivenSampleQuestion();
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new UpdateQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenInformation_WhenUpdatingTheQuestionDoesNotExist_ThenItShouldReturnError()
        {
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new UpdateQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist.";
            Assert.Equal(message, result.Message);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "This is a question text";
            int point = 4;
            return new Question(questionText, point, QuestionType.MultipChoice);
        }
    }
}
