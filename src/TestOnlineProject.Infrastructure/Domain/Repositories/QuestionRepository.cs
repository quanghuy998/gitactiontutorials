using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class QuestionRepository : BaseRepository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {

        }

        Task<Question> IRepository<Question, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Question>(x => x.Id == id);
            spec.Includes.Add(x => x.Choices);
            spec.Includes.Add(x => x.Exams);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}
