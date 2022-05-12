using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Tests
{
    public class AddQuestionToTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new Mock<ITestRepository>();
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();

        private AddQuestionToTestCommand command = new AddQuestionToTestCommand()
        {
            TestId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid()
        };

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToTest_ThenItShouldReturnSuccess()
        {
            var test = GivenSampleTest();
            var question = GivenSampleQuestion();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new AddQuestionToTestCommandHandler(mockTestRepository.Object, mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToTheTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            var question = GivenSampleQuestion();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new AddQuestionToTestCommandHandler(mockTestRepository.Object, mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenAnQuestionDoesNotExist_WhenAddingQuestionToTest_ThenItShouldReturnErrorMessage()
        {
            var test = GivenSampleTest();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new AddQuestionToTestCommandHandler(mockTestRepository.Object, mockQuestionRepository.Object);

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

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
