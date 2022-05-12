using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class RemoveQuestionFromTestCommand : ICommand
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
    }

    public class RemoveQuestionFromTestCommandHandler : ICommandHandler<RemoveQuestionFromTestCommand>
    {
        private readonly ITestRepository _testRepository;

        public RemoveQuestionFromTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<CommandResult> Handle(RemoveQuestionFromTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult.Error("The test does not exist.");

            var result = test.Questions.Find(x => x.Id == request.QuestionId);
            if (result is null) return CommandResult.Error("The question does not exist in this test.");

            test.RemoveQuestion(request.QuestionId);
            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult.Success();
        }
    }
}
