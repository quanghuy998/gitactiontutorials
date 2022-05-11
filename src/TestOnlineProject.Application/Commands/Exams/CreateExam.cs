using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Exams
{
    public class CreateExamCommand : ICommand
    {
        public string Title { get; init; }
    }

    public class CreateExamCommandHandler : ICommandHandler<CreateExamCommand>
    {
        private readonly IExamRepository _examRepository;
        
        public CreateExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;            
        }

        public async Task<CommandResult> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var exam = new Exam(request.Title);
            await _examRepository.AddAsync(exam, cancellationToken);

            return CommandResult.Success();
        }
    }
}
