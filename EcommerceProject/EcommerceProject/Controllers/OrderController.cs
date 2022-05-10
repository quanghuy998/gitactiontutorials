using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Orders.ChangeOrderStatus;
using EcommerceProject.Application.Queries.Orders.GetOrderDetails;
using EcommerceProject.Application.Queries.Orders.GetOrders;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.API.Controllers
{
    
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        
        public OrderController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        [Route("get-all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = new GetOrdersQuery { UserId = request.UserId };
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerOrders([FromQuery] GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = new GetCustomerOrdersQuery { UserId = request.UserId };
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }
        
        [HttpGet]
        [Route("{orderId}")]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] Guid customerId
                                                ,[FromRoute] int orderId
                                                ,CancellationToken cancellationToken)
        {
            var query = new GetOrderDetailsQuery 
            {
                 UserId = customerId,
                 OrderId = orderId
            };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPut]
        [Route("{orderId}/change-orderstatus")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderId
                                                ,[FromBody] ChangeOrderStatusRequest request
                                                ,CancellationToken cancellationToken)
        {
            var command = new ChangeOrderStatusCommand { OrderId = orderId, OrderStatus = request.OrderStatus };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
