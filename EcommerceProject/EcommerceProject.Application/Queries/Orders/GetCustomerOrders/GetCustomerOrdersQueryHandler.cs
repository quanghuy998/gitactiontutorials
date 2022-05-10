using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Orders.GetOrders
{
    public class GetCustomerOrdersQueryHandler : IQueryHandler<GetCustomerOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public GetCustomerOrdersQueryHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetCustomerOrdersQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindOneAsync(query.UserId, cancellationToken);
            if (user == null) return null;

            var spec = new SpecificationBase<Order>(x => x.UserId == query.UserId);
            return await _orderRepository.FindAllAsync(spec, cancellationToken);
        }
    }
}
