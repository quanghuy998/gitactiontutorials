using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.ExamAggregate
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
