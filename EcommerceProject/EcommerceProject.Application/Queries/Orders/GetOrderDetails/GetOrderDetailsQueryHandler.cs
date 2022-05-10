using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Orders.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<Order> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindOneAsync(query.UserId, cancellationToken);
            if (user == null) return null;

            return await _orderRepository.FindOneAsync(query.OrderId, cancellationToken);
        }
    }
}
