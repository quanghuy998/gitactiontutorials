using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.SharedKermel;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Orders.GetOrders
{
    public class GetOrdersQuery : IQuery<IEnumerable<Order>>
    {
        public Guid UserId { get; init; }
    }
}
