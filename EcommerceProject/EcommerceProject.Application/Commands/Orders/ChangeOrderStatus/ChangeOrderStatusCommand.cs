using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Orders.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand : ICommand<int>
    {
        public int OrderId { get; init; }
        public OrderStatus OrderStatus { get; init; }
    }
}
