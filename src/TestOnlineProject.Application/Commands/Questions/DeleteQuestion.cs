using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class DeleteQuestionCommand : ICommand
    {
        public Guid Id { get; init; }
    }

    public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindOneAsync(request.Id, cancellationToken);
            if (question is null) return CommandResult.Error("Question does not exist.");

            await _questionRepository.DeleteAsync(question, cancellationToken);
            return CommandResult.Success();
        }
    }
}
