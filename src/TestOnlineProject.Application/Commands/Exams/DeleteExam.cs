using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Exams
{
    public class DeleteExamCommand : ICommand
    {
        public Guid Id { get; init; }
    }

    public class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand>
    {
        private readonly IExamRepository _examRepository;
        
        public DeleteExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }
        public async Task<CommandResult> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.FindOneAsync(request.Id, cancellationToken);
            if (exam == null) return CommandResult.Error("Exam does not exist.");

            await _examRepository.DeleteAsync(exam, cancellationToken);
            return CommandResult.Success();
        }
    }
}
