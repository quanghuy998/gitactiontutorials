using TestOnlineProject.Domain.Aggregates.QuestionAggregate;

namespace TestOnlineProject.API.Dtos
{
    public class ExamRequest
    {
        public record CreateExamRequest(string title);
        public record UpdateExamRequest(Guid id, string title);
        public record AddQuestionToExamRequest(Guid examId, Guid questionId, string questionText, QuestionType questionType, int point);
        public record RemoveQuestionFromExamRequest(Guid examId, Guid questionId);

    }
}
