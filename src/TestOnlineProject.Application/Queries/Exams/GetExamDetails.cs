using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Exams
{
    public class GetExamDetailsQuery : IQuery<Exam>
    {
        public Guid Id { get; set; }
    }

    public class GetExamDetailsQueryHandler : IQueryHandler<GetExamDetailsQuery, Exam>
    {
        private readonly IExamRepository _examRepository;

        public GetExamDetailsQueryHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<Exam> Handle(GetExamDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _examRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
