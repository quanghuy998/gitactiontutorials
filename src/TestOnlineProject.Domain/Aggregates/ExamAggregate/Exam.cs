﻿using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.ExamAggregate
{
    public class Exam : AggregateRoot<Guid>
    {
        public string Title { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public List<Question> Questions { get; }

        private Exam()
        {

        }
        
        public Exam(string title)
        {
            Title = title;
            ModifiedDate = DateTime.Now;
            Questions = new();
        }

        public void UpdateExam(string title)
        {
            Title = title;
            ModifiedDate = DateTime.Now;
        }

        public void AddQuestion(Question request)
        {
            Questions.Add(request);
            ModifiedDate = DateTime.Now;
        }

        public void RemoveQuestion(Guid questionId)
        {
            var question = Questions.Find(x => x.Id == questionId);
            Questions.Remove(question);
            ModifiedDate = DateTime.Now;
        }
    }
}
