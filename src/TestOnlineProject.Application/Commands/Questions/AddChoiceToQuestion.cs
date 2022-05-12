using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Questions
{
    public class AddChoiceToQuestionCommand : ICommand
    {
        public Guid QuestionId { get; init; }
        public string ChoiceText { get; init; }
        public bool IsCorrect { get; init; }
    }

    public class AddChoiceToQuestionCommandHandler : ICommandHandler<AddChoiceToQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;

        public AddChoiceToQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CommandResult> Handle(AddChoiceToQuestionCommand request, CancellationToken cancellationToken)
        {
            var choice = new Choice(request.ChoiceText, request.IsCorrect);
            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if (question is null) return CommandResult.Error("The question does not exist.");

            if (question.QuestionType == QuestionType.MultipChoice)
            {
                question.AddChoiceToQuestion(choice);
            }
            await _questionRepository.SaveAsync(question, cancellationToken);

            return CommandResult.Success();
        }
    }
}
