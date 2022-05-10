using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.SharedKermel;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Products.GetProducts
{
    public class GetProductsQuery : IQuery<IEnumerable<Product>>
    {
        public string Name { get; init; }
        public string TradeMark { get; init; }
        public string Origin { get; init; }
        public MoneyValue MaxValue { get; init; }
        public MoneyValue MinValue { get; init; }
    }
}
