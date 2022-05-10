using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CommandResult<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindOneAsync(command.ProductId, cancellationToken);
            if (product is null) return CommandResult<int>.Error($"Product with id {command.ProductId} does not exist.");

            product.UpdateProduct(command.Name, command.Price, command.TradeMark, command.Origin, command.Discription);
            await _productRepository.SaveAsync(product, cancellationToken);

            return CommandResult<int>.Success(command.ProductId);
        }
    }
}
