using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Questions
{
    public class GetQuestionDetailsQuery : IQuery<Question>
    {
        public Guid Id { get; init; }
    }

    public class GetQuestionDetailsQueryHandler : IQueryHandler<GetQuestionDetailsQuery, Question>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionDetailsQueryHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<Question> Handle(GetQuestionDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _questionRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
