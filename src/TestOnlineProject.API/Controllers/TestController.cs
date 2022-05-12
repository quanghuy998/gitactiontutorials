using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public TestController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTests(CancellationToken cancellationToken)
        {
            var query = new GetAllTestsQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTestDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTestDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTestCommand() { Title = request.title };
            var result = await _commandBus.SendAsync(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTest([FromRoute] Guid id,
                                    [FromBody] UpdateTestRequest request, 
                                    CancellationToken cancellationToken)
        {
            var command = new UpdateTestCommand()
            {
                Id = id,
                Title = request.title
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTest([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTestCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("{testId}/questions/{questionId}")]
        public async Task<IActionResult> AddQuestionToTest([FromRoute] Guid testId, 
                                                [FromRoute] Guid questionId, 
                                                CancellationToken cancellationToken)
        {
            var command = new AddQuestionToTestCommand()
            {
                TestId = testId,
                QuestionId = questionId,
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        [Route("{testId}/questions/{questionId}")]
        public async Task<IActionResult> RemoveQuestionFromTest([FromRoute] Guid testId,
                                                [FromRoute] Guid questionId, 
                                                CancellationToken cancellationToken)
        {
            var command = new RemoveQuestionFromTestCommand()
            {
                TestId = testId,
                QuestionId = questionId
            };

            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
