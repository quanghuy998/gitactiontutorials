using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Carts.RemoveProductFromCart
{
    public class RemoveProductFromCartCommand : ICommand<Guid>
    {
        public Guid UserId { get; init; }
        public int CartProductId { get; init; }
    }
}
