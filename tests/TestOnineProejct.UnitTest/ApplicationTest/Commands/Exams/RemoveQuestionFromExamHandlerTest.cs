using Moq;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;

namespace TestOnineProejct.UnitTest.ApplicationTest.Commands.Exams
{
    public class RemoveQuestionFromExamHandlerTest
    {
        private Mock<IExamRepository> mockExamRepository = new Mock<IExamRepository>();
        private Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
    }
}
