using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Products.GetProducts
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<Product>(x => (query.Name == null || x.Name == query.Name) 
                                                        && (query.TradeMark == null || x.TradeMark == query.TradeMark)
                                                        && (query.Origin == null || x.Origin == query.Origin)
                                                        && (query.MaxValue == null || x.Price.Value <= query.MaxValue.Value)
                                                        && (query.MinValue == null || x.Price.Value >= query.MaxValue.Value));

            return await _productRepository.FindAllAsync(spec, cancellationToken);
        }
    }
}