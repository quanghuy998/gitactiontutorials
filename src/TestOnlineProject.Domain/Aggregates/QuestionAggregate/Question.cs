using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.QuestionAggregate
{
    public class Question : AggregateRoot<Guid>
    {
        public string QuestionText { get; }
        public int Point { get; }
        public QuestionType QuestionType { get; }
        List<Choice> Choices { get; }
        List<Exam> Exams { get; }

        private Question()
        {

        }

        public Question(string questionTest, int point, QuestionType questionType)
        {
            QuestionText = questionTest;
            QuestionType = questionType;
            Point = point;
            Choices = new();
            Exams = new();
        }

        public void AddChoice(Choice choice)
        {
            Choices.Add(choice);
        }
    }
}
