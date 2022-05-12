using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class CreateTestCommand : ICommand
    {
        public string Title { get; init; }
    }

    public class CreateTestCommandHandler : ICommandHandler<CreateTestCommand>
    {
        private readonly ITestRepository _testRepository;
        
        public CreateTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;            
        }

        public async Task<CommandResult> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = new Test(request.Title);
            await _testRepository.AddAsync(test, cancellationToken);

            return CommandResult.Success();
        }
    }
}
