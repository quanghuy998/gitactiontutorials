using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Exams;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Exams
{
    public class DeleteExamHandlerTest
    {
        private Mock<IExamRepository> mockExamRepository = new Mock<IExamRepository>();
        private DeleteExamCommand command = new DeleteExamCommand() { Id = Guid.NewGuid();

        [Fact]
        public async Task GivenInformation_WhenDeletingAnExam_ThenItShouldReturnSuccess()
        {
            var exam = GivenSampleExam();
            mockExamRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(exam);
            var handler = new DeleteExamCommandHandler(mockExamRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        private Exam GivenSampleExam()
        {
            string title = "Exam 1";
            return new Exam(title);
        }
    }
}
