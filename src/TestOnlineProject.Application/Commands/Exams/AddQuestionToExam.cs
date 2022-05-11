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
    public class AddQuestionToExamCommand : ICommand
    {
        public Guid ExamId { get; init; }
        public Guid QuestionId { get; init; }
        public string QuestionText { get; init; }
        public QuestionType QuestionType { get; init; }
        public int Point { get; init; }
    }

    public class AddQuestionToExamCommandHandler : ICommandHandler<AddQuestionToExamCommand>
    {
        private readonly IExamRepository _examRepository;
        private readonly IQuestionRepository _questionRepository;
        
        public AddQuestionToExamCommandHandler(IExamRepository examRepository, IQuestionRepository questionRepository)
        {
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }
        
        public async Task<CommandResult> Handle(AddQuestionToExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.FindOneAsync(request.ExamId, cancellationToken);
            if (exam == null) return CommandResult.Error("Exam does not exist.");

            var question = await _questionRepository.FindOneAsync(request.QuestionId, cancellationToken);
            if(question == null)
            {
                question = new Question(request.QuestionText, request.Point, request.QuestionType);
            }

            exam.AddQuestion(question);
            await _examRepository.SaveAsync(exam, cancellationToken);

            return CommandResult.Success();
        }
    }
}
