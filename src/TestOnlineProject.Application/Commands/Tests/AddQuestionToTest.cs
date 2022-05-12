using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class AddQuestionToTestCommand : ICommand
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
    }

    public class AddQuestionToTestCommandHandler : ICommandHandler<AddQuestionToTestCommand>
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        
        public AddQuestionToTestCommandHandler(ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
        }
        
        public async Task<CommandResult> Handle(AddQuestionToTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test == null) return CommandResult.Error("The test does not exist.");

            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if (question == null) return CommandResult.Error("The question does not exist.");

            test.AddQuestion(question);
            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult.Success();
        }
    }
}
