using Moq;
using System.Threading;
using TestOnlineProject.Application.Commands.Exams;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Exams
{
    public class CreateExamHandlerTest
    {
        private Mock<IExamRepository> mockExamRepository = new Mock<IExamRepository>();
        private CreateExamCommand command = new CreateExamCommand() { Title = "Is this a title?" };

        [Fact]
        public async void GivenInformation_WhenCreatingAnExam_ThenItShouldReturnSuccess()
        {
            var handler = new CreateExamCommandHandler(mockExamRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

       
    }
}
