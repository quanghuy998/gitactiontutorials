using EcommerceProject.Infrastructure.CQRS.Queries;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;

namespace EcommerceProject.Application.Queries.Carts.GetCartDetails
{
    public class GetCartDetailsQuery : IQuery<Cart>
    {
        public Guid UserId { get; init; }
    }
}
