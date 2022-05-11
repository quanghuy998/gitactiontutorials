using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Dtos;
using TestOnlineProject.Application.Commands.Users;
using TestOnlineProject.Application.Queries.Users;
using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public UserController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUser(CancellationToken cancellationToken)
        {
            var query = new GetAllUserQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserDetailQuery() { UserId = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound("Customer is not exist.");

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand()
            {
                UserName = request.username,
                Email = request.email,
                Name = request.name,
                Password = request.password
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var query = new AuthenticateUserQuery()
            {
                UserName = request.username,
                Password = request.password
            };

            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return BadRequest("User name or password is incorrectly.");

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand() { UserId = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
