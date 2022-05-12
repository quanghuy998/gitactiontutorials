using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public class Test : AggregateRoot<Guid>
    {
        public string Title { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public bool IsPublish { get; private set; }
        public List<Question> Questions { get; }

        private Test()
        {

        }
        
        public Test(string title)
        {
            Title = title;
            ModifiedDate = DateTime.Now;
            IsPublish = false;
            Questions = new();
        }

        public void UpdateTest(string title, bool isPublish)
        {
            Title = title;
            IsPublish = isPublish;
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
