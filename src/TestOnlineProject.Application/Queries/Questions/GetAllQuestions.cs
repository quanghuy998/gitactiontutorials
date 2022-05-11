using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Questions
{
    public class GetAllQuestionsQuery : IQuery<IEnumerable<Question>>
    {

    }

    public class GetAllQuestionsQueryHandler : IQueryHandler<GetAllQuestionsQuery, IEnumerable<Question>>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetAllQuestionsQueryHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<Question>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            return await _questionRepository.FindAllAsync(null, cancellationToken);
        }
    }
}
