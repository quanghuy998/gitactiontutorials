using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.ExamAggregate
{
    public class Question : Entity<Guid>
    {
        public string QuestionText { get; }
        public int Point { get; }
        public QuestionType QuestionType { get; }
        List<Choice> Choices { get; }

        private Question()
        {

        }

        public Question(string questionTest, int point, QuestionType questionType)
        {
            QuestionText = questionTest;
            QuestionType = questionType;
            Point = point;
            Choices = new();
        }

        public void AddChoice(Choice choice)
        {
            Choices.Add(choice);
        }
    }
}
