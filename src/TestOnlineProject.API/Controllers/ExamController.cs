using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Application.Commands.Exams;
using TestOnlineProject.Application.Queries.Exams;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.ExamRequest;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public ExamController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams(CancellationToken cancellationToken)
        {
            var query = new GetAllExamsQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExamDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetExamDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateExamCommand() { Title = request.title };
            var result = await _commandBus.SendAsync(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExam([FromBody] UpdateExamRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateExamCommand()
            {
                Id = request.id,
                Title = request.title
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteExam([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteExamCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("add-question-to-exam")]
        public async Task<IActionResult> AddQuestionToExam([FromBody] AddQuestionToExamRequest request, CancellationToken cancellationToken)
        {
            var command = new AddQuestionToExamCommand()
            {
                QuestionText = request.questionText,
                ExamId = request.examId,
                Point = request.point,
                QuestionId = request.questionId,
                QuestionType = request.questionType
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        [Route("remove-question-from-exam")]
        public async Task<IActionResult> RemoveQuestionFromExam([FromBody] RemoveQuestionFromExamRequest request, CancellationToken cancellationToken)
        {
            var command = new RemoveQuestionFromExamCommand()
            {
                ExamId = request.examId,
                QuestionId = request.questionId
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
