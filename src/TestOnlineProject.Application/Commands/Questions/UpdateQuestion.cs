using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class UpdateQuestionCommand : ICommand
    {
        public Guid QuestionId { get; init; }
        public string QuestionText { get; init; }
        public QuestionType QuestionType { get; init; }
        public int Point { get; init; }
    }

    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if (question is null) return CommandResult.Error("Question does not exist.");

            question.UpdateQuestion(request.QuestionText, request.QuestionType, request.Point);
            await _questionRepository.SaveAsync(question, cancellationToken);

            return CommandResult.Success();
        }
    }
}
