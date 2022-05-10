using EcommerceProject.API.Dtos;
using EcommerceProject.Application.Commands.Products.CreateProduct;
using EcommerceProject.Application.Commands.Products.DeleteProduct;
using EcommerceProject.Application.Commands.Products.UpdateProduct;
using EcommerceProject.Application.Queries.Products.GetProductById;
using EcommerceProject.Application.Queries.Products.GetProducts;
using EcommerceProject.Domain.SharedKermel;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.API.Controllers
{
    //[Authorize(Roles = "Customer,Admin")]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public ProductController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
        {
            MoneyValue maxValue = null;
            MoneyValue minValue = null;
            if (request.MaxValue != 0) maxValue = MoneyValue.Of(request.MaxValue, request.Currency);
            if (request.MinValue != 0) minValue = MoneyValue.Of(request.MinValue, request.Currency);

            var query = new GetProductsQuery()
            {
                Name = request.Name,
                Origin = request.Origin,
                TradeMark = request.TradeMark,
                MaxValue = maxValue,
                MinValue = minValue
            };
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> GetProductDetails([FromRoute] int productId, CancellationToken cancellationToken)
        {
            var query = new GetProductDetailsQuery { ProductId = productId };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand
            {
                Name = request.Name,
                Price = MoneyValue.Of(request.MoneyValue, request.Currency),
                TradeMark = request.TradeMark,
                Origin = request.Origin,
                Discription = request.Discription
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId
                                                ,[FromBody] UpdateProductRequest request
                                                ,CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand
            {
                ProductId = productId,
                Name = request.Name,
                Price = MoneyValue.Of(request.Value, request.Currency),
                TradeMark = request.TradeMark,
                Origin = request.Origin,
                Discription = request.Discription
            };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { ProductId = productId };
            var result = await _commandBus.SendAsyns(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }
    }
}
