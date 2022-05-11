﻿using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.ExamAggregate
{
    public class Exam : AggregateRoot<Guid>
    {
        public string Title { get; }
        public DateTime ModifiedDate { get; }
        List<Question> Questions { get; }

        private Exam()
        {

        }

        public Exam(string title)
        {
            Title = title;
            ModifiedDate = DateTime.Now;
            Questions = new();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }
}
