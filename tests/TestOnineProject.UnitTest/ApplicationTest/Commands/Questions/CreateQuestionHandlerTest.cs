using Moq;
using System.Threading;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.ApplicationTest.Commands.Questions
{
    public class CreateQuestionHandlerTest
    {
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private CreateQuestionCommand command = new CreateQuestionCommand()
        {
            QuestionText = "Question number 1.",
            Point = 5,
            QuestionType = QuestionType.MultipChoice
        };

        [Fact]
        public async void GivenInformation_WhenCreatingAnQuestion_ThenItShouldReturnSuccess()
        {
            var handler = new CreateQuestionCommandHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
    }
}
