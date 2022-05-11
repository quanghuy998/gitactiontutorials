using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class RemoveChoiceFromQuestionCommand : ICommand
    {
        public Guid QuestionId { get; init; }
        public Guid ChoiceId { get; init; }
    }

    public class RemoveChoiceFromQuestionCommandHandler : ICommandHandler<RemoveChoiceFromQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public RemoveChoiceFromQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(RemoveChoiceFromQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if (question is null) return CommandResult.Error("Question does not exist.");

            await _questionRepository.DeleteAsync(question, cancellationToken);
            return CommandResult.Success();
        }
    }
}
