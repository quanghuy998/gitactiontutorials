using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.QuestionAggregate
{
    public class Question : AggregateRoot<Guid>
    {
        public string QuestionText { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public int Point { get; private set; }
        public List<Choice> Choices { get; }
        public List<Exam> Exams { get; }

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

        public void UpdateQuestion(string questionText, QuestionType questionType, int point)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            Point = point;
        }

        public void AddChoice(Choice request)
        {
            Choices.Add(request);
        }
        
        public void RemoveChoice(Choice request)
        {
            var choice = Choices.Find(x => x.Id == request.Id);
            Choices.Remove(choice);
        }
    }
}
