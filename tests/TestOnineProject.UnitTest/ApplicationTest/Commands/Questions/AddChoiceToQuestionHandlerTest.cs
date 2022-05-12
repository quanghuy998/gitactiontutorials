using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.ApplicationTest.Commands.Questions
{
    public class AddChoiceToQuestionHandlerTest
    {
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private AddChoiceToQuestionCommand command = new AddChoiceToQuestionCommand()
        {
            QuestionId = Guid.NewGuid(),
            ChoiceText = "This is a choice text",
            IsCorrect = true
        };

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToQuestion_ThenItShouldReturnSuccess()
        {
            var question = GivenSampleQuestion();
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new AddChoiceToQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToTheQuestionDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new AddChoiceToQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist.";
            Assert.Equal(message, result.Message);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }
    }
}
