using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Orders.GetOrders
{
    public class GetCustomerOrdersQuery : IQuery<IEnumerable<Order>>
    {
        public Guid UserId { get; init; }
    }
}
