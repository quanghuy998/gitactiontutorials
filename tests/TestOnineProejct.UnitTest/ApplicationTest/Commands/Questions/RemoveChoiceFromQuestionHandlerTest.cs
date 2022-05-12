using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Questions
{
    public class RemoveChoiceFromQuestionHandlerTest
    {
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private RemoveChoiceFromQuestionCommand command = new RemoveChoiceFromQuestionCommand()
        {
            QuestionId = Guid.NewGuid(),
            ChoiceId = Guid.NewGuid()
        };

        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromQuestion_ThenItShouldReturnSuccess()
        {
            var question = GivenSampleQuestion();
            var choice = GivenSampleChoice();
            choice.Id = command.ChoiceId;
            question.AddChoiceToQuestion(choice);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new RemoveChoiceFromQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromTheQuestionDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new RemoveChoiceFromQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact] 
        public async Task GivenInformationOfTheChoiceDoesNotExist_WhenRemovingChoiceFromTheQuestion_ThenItShouldReturnErrorMesage()
        {
            var question = GivenSampleQuestion();
            var choice = GivenSampleChoice();
            choice.Id = Guid.NewGuid();
            question.AddChoiceToQuestion(choice);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new RemoveChoiceFromQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The choice does not exist in this question.";
            Assert.Equal(message, result.Message);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }

        private Choice GivenSampleChoice()
        {
            string choiceText = "Choice number 1?";
            bool isCorrect = true;
            return new Choice(choiceText, isCorrect);
        }
    }
}
