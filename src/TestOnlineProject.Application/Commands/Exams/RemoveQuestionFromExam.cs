using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Exams
{
    public class RemoveQuestionFromExamCommand : ICommand
    {
        public Guid ExamId { get; init; }
        public Guid QuestionId { get; init; }
    }

    public class RemoveQuestionFromExamCommandHandler : ICommandHandler<RemoveQuestionFromExamCommand>
    {
        private readonly IExamRepository _examRepository;
        private readonly IQuestionRepository _questionRepository;

        public RemoveQuestionFromExamCommandHandler(IExamRepository examRepository, IQuestionRepository questionRepository)
        {
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }
        public async Task<CommandResult> Handle(RemoveQuestionFromExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.FindOneAsync(request.ExamId, cancellationToken);
            if (exam is null) return CommandResult.Error("Exam does not exist.");

            var question = await _questionRepository.FindOneAsync(request.QuestionId);
            if (question is null) return CommandResult.Error("Question does not exist.");

            var result = exam.Questions.Find(x => x.Id == request.QuestionId);
            if (result is null) return CommandResult.Error("Question does not exist in this exam.");
            exam.RemoveQuestion(request.QuestionId);
            await _examRepository.SaveAsync(exam, cancellationToken);

            return CommandResult.Success();
        }
    }
}
