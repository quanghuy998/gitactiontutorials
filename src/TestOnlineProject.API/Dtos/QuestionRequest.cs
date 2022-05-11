using TestOnlineProject.Domain.Aggregates.QuestionAggregate;

namespace TestOnlineProject.API.Dtos
{
    public class QuestionRequest
    {
        public record CreateQuestionRequest(string questionText, QuestionType questionType, int point);
        public record UpdateQuestionRequest(string questionText, QuestionType questionType, int point);
        public record AddChoiceToQuestionRequest(string choiceText, bool isCorrect);
        public record UpdateChoiceInQuestionRequest(string choiceText, bool isCorrect);

    }
}
