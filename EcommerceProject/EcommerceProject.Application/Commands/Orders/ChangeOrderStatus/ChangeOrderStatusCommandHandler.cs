using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Orders.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, int>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CommandResult<int>> Handle(ChangeOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindOneAsync(command.OrderId, cancellationToken);
            if (order == null) return CommandResult<int>.Error("Order is not exist.");

            order.ChangeOrderStatus(command.OrderStatus);
            await _orderRepository.SaveAsync(order);

            return CommandResult<int>.Success(order.Id);
        }
    }
}
