using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CommandResult<int>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindOneAsync(command.ProductId, cancellationToken);
            await _productRepository.DeleteAsync(product);

            return CommandResult<int>.Success(command.ProductId);
        }
    }
}
