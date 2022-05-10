using EcommerceProject.Domain.SeedWork;

namespace EcommerceProject.Domain.AggregatesRoot.OrderAggregate
{
    public interface IOrderRepository : IBaseRepository<Order, int>
    {
    }
}
