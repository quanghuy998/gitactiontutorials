using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Products.GetProductById
{
    public class GetProductDetailsQueryHandler : IQueryHandler<GetProductDetailsQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductDetailsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductDetailsQuery query, CancellationToken cancellationToken)
        {
            return await _productRepository.FindOneAsync(query.ProductId, cancellationToken);
        }
    }
}
