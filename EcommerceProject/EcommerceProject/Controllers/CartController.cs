using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Carts.AddProductToCart;
using EcommerceProject.Application.Commands.Carts.ChangeProductQuantity;
using EcommerceProject.Application.Commands.Carts.PlaceOrder;
using EcommerceProject.Application.Commands.Carts.RemoveProductFromCart;
using EcommerceProject.Application.Queries.Carts.GetCartDetails;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceProject.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/customers/{customerId}/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public CartController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartDetails([FromRoute] Guid customerId
                                                ,CancellationToken cancellationToken)
        {
            var query = new GetCartDetailsQuery
            {
                UserId = customerId
            };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound("Customer is not exist.");

            return Ok(result);
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> AddProductToCart([FromRoute] Guid customerId
                                                ,[FromBody] AddProductToCartRequest request
                                                ,CancellationToken cancellationToken)
        {
            var command = new AddProductToCartCommand
            {
                UserId = customerId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        [Route("products")]
        public async Task<IActionResult> ChangeProductQuantity([FromRoute] Guid customerId
                                                    ,[FromBody] ChangeProductQuantityRequest request
                                                    ,CancellationToken cancellationToken)
        {
            var command = new ChangeProductQuantityCommand
            {
                UserId = customerId,
                CartProductId = request.CartProductId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("products")]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] Guid customerId
                                                ,[FromBody] RemoveProductFromCartRequest request
                                                ,CancellationToken cancellationToken)
        {
            var command = new RemoveProductFromCartCommand 
            { 
                UserId = customerId,
                CartProductId = request.CartProductId 
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid customerId
                                            ,[FromBody] PlaceOrderRequest request
                                            ,CancellationToken cancellationToken)
        {
            var command = new PlaceOrderCommand
            {
                UserId = customerId,
                ShippingAddress = request.ShippingAddress,
                ShippingPhoneNumber = request.ShippingPhoneNumber
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
