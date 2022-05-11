using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.QuestionAggregate
{
    public class Choice : Entity<Guid>
    {
        public string ChoiceText { get; }
        public bool IsCorrect { get; }

        private Choice()
        {

        }

        public Choice(string choiceText, bool isCorrect)
        {
            ChoiceText = choiceText;
            IsCorrect = isCorrect;
        }
    }
}
