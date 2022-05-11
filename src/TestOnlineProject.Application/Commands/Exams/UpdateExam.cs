using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Exams
{
    public class UpdateExamCommand : ICommand
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
    }

    public class UpdateExamCommandHandler : ICommandHandler<UpdateExamCommand>
    {
        private readonly IExamRepository _examRepository;

        public UpdateExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<CommandResult> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.FindOneAsync(request.Id, cancellationToken);
            if (exam == null) return CommandResult.Error("Exam does not exist.");
            
            exam.UpdateExam(request.Title);
            await _examRepository.SaveAsync(exam, cancellationToken);

            return CommandResult.Success();
        }
    }
}
