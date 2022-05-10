using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Users.AuthenticateCustomer;
using EcommerceProject.Application.Commands.Users.RegisterCustomer;
using EcommerceProject.Application.Queries.Users.GetCustomerDetails;
using EcommerceProject.Application.Queries.Users.GetCustomers;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public UserController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var query = new GetUsersQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserDetails([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserDetailsQuery() { UserId = userId };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
         {
            var command = new RegisterUserCommand 
            { 
                UserName = request.UserName, 
                Password = request.Password, 
                Name = request.Name, 
                Email = request.Email, 
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(new { customerId = result.Response.UserId, cartId = result.Response.CartId });
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var command = new AuthenticateUserCommand
            {
                Password = request.Password,
                UserName = request.UserName,
                RememberMe = request.RememberMe
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(new { token = result.Response });
        }
    }
}
