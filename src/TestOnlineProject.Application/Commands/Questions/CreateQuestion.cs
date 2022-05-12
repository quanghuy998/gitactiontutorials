using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class CreateQuestionCommand : ICommand
    {
        public string QuestionText { get; init; }
        public QuestionType QuestionType { get; init; }
        public int Point { get; init; }
        public int TimeLimit { get; init; }
    }

    public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question(request.QuestionText, request.Point, request.TimeLimit, request.QuestionType);
            await _questionRepository.AddAsync(question, cancellationToken);

            return CommandResult.Success();
        }
    }
}
