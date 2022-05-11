using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Exams;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Exams
{
    public class AddQuestionToExamHandlerTest
    {
        private Mock<IExamRepository> mockExamRepository = new Mock<IExamRepository>();
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();

        private AddQuestionToExamCommand command = new AddQuestionToExamCommand()
        {
            ExamId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid()
        };

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToExam_ThenItShouldReturnSuccess()
        {
            var exam = GivenSampleExam();
            var question = GivenSampleQuestion();
            mockExamRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(exam);
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new AddQuestionToExamCommandHandler(mockExamRepository.Object, mockQuestionRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }

        private Exam GivenSampleExam()
        {
            string title = "Exam 1";
            return new Exam(title);
        }
    }
}
