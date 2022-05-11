using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Exams
{
    public class GetAllExamsQuery : IQuery<IEnumerable<Exam>>
    {

    }

    public class GetAllExamsQueryHandler : IQueryHandler<GetAllExamsQuery, IEnumerable<Exam>>
    {
        private readonly IExamRepository _examRepository;

        public GetAllExamsQueryHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<IEnumerable<Exam>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
             return await _examRepository.FindAllAsync(null, cancellationToken);
        }
    }
}
