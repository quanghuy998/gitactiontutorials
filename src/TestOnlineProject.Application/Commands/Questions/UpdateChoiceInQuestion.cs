using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class UpdateChoiceInQuestionCommand : ICommand
    {
        public Guid QuestionId { get; init; }
        public Guid ChoiceId { get; init; }
        public string ChoiceText { get; init; }
        public bool IsCorrect { get; init; }
    }

    public class UpdateChoiceQuestionCommandHandler : ICommandHandler<UpdateChoiceInQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateChoiceQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<CommandResult> Handle(UpdateChoiceInQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if (question is null) return CommandResult.Error("Question does not exist.");

            question.UpdateQuestion(question.QuestionText, question.QuestionType, question.Point);
            await _questionRepository.SaveAsync(question, cancellationToken);

            return CommandResult.Success();
        }
    }
}
