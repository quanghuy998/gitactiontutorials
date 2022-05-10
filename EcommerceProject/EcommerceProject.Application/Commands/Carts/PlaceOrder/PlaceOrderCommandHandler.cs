using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Carts.PlaceOrder
{
    public class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderCommandHandler(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CommandResult> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<Cart>(x => x.UserId == command.UserId);
            var cart = await _cartRepository.FindOneAsync(spec, cancellationToken);
            if (cart == null) return CommandResult.Error("Do not find a cart with customer id.");

            var orderProducts = new List<OrderProduct>();
            var cartProducts = cart.CartProducts;
            foreach(var cartProduct in cartProducts)
            {
                OrderProduct orderProduct = new OrderProduct(cartProduct.ProductId, cartProduct.Quantity, cartProduct.Price);
                orderProducts.Add(orderProduct);
            }
            
            var order = new Order(cart.UserId, command.ShippingAddress, command.ShippingPhoneNumber, orderProducts);
            await _orderRepository.AddAsync(order, cancellationToken);

            cart.RemoveAllCartProduct();
            await _cartRepository.SaveAsync(cart);

            return CommandResult.Success();
        }
    }
}
