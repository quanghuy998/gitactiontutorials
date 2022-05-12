using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.QuestionAggregate
{
    public class Question : AggregateRoot<Guid>
    {
        public string QuestionText { get; private set; }
        public int Point { get; private set; }
        public int TimeLimit { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public List<Choice> Choices { get; }
        public List<Test> Tests { get; }

        private Question()
        {

        }

        public Question(string questionTest, int point, int timeLitmit, QuestionType questionType)
        {
            QuestionText = questionTest;
            Point = point;
            TimeLimit = timeLitmit;
            QuestionType = questionType;
            Choices = new();
            Tests = new();
        }

        public void UpdateQuestion(string questionText, QuestionType questionType, int point, int timeLitmit)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            Point = point;
            TimeLimit = timeLitmit;
        }

        public void AddChoiceToQuestion(Choice request)
        {
            Choices.Add(request);
        }

        public void UpdateChoiceInQuestion(Choice request)
        {
            var choice = Choices.Find(x => x.Id == request.Id);
            choice.UpdateChoice(request.ChoiceText, request.IsCorrect);
        }

        public void RemoveChoiceFromQuestion(Choice request)
        {
            var choice = Choices.Find(x => x.Id == request.Id);
            Choices.Remove(choice);
        }
    }
}
