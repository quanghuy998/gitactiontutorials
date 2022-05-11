using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class ExamRepository : BaseRepository<Exam, Guid>, IExamRepository
    {
        public ExamRepository(AppDbContext context) : base(context)
        {

        }

        Task<Exam> IRepository<Exam, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Exam>(p => p.Id == id);
            spec.Includes.Add(p => p.Questions);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}
