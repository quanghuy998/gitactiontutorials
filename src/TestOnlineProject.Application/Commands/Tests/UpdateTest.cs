using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class UpdateTestCommand : ICommand
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public bool IsPublish { get; init; }
    }

    public class UpdateTestCommandHandler : ICommandHandler<UpdateTestCommand>
    {
        private readonly ITestRepository _testRepository;

        public UpdateTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.Id, cancellationToken);
            if (test == null) return CommandResult.Error("The test does not exist.");

            test.UpdateTest(request.Title, request.IsPublish);
            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult.Success();
        }
    }
}
