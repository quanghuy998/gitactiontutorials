using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Application.Commands.Questions;
using TestOnlineProject.Application.Queries.Questions;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.QuestionRequest;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public QuestionController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions(CancellationToken cancellationToken)
        {
            var query = new GetAllQuestionsQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQuestionDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetQuestionDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateQuestionCommand() 
            {
                QuestionText = request.questionText,
                Point = request.point,
                QuestionType = request.questionType
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] Guid id, 
                            [FromBody] UpdateQuestionRequest request, 
                            CancellationToken cancellationToken)
        {
            var command = new UpdateQuestionCommand()
            {
                QuestionId = id,
                QuestionText = request.questionText,
                Point = request.point,
                QuestionType = request.questionType
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteQuestionCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("{questionId}/choices")]
        public async Task<IActionResult> AddChoiceToQuestion([FromRoute] Guid questionId,
                                                [FromBody] AddChoiceToQuestionRequest request,
                                                CancellationToken cancellationToken)
        {
            var command = new AddChoiceToQuestionCommand()
            {
                QuestionId = questionId,
                ChoiceText = request.choiceText,
                IsCorrect = request.isCorrect
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        [Route("{questionId}/choices/{choiceId}")]
        public async Task<IActionResult> UpdateChoiceInQuestion([FromRoute] Guid questionId,
                                                [FromRoute] Guid choiceId,
                                                UpdateChoiceInQuestionRequest request,
                                                CancellationToken cancellationToken)
        {
            var command = new UpdateChoiceInQuestionCommand()
            {
                ChoiceId = choiceId,
                QuestionId = questionId,
                ChoiceText = request.choiceText,
                IsCorrect = request.isCorrect
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{questionId}/choices/{choiceId}")]
        public async Task<IActionResult> RemoveChoiceFromQuestion([FromRoute] Guid questionId,
                                                [FromRoute] Guid choiceId,
                                                CancellationToken cancellationToken)
        {
            var command = new RemoveChoiceFromQuestionCommand()
            {
                ChoiceId = choiceId,
                QuestionId = questionId
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
