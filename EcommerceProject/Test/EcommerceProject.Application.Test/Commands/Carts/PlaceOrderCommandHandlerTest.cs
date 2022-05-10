using EcommerceProject.Application.Commands.Carts.PlaceOrder;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Application.Test.Commands.Carts
{
    public class PlaceOrderCommandHandlerTest
    {
        private readonly Mock<ICartRepository> mockCartRepository = new Mock<ICartRepository>();
        private readonly Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
        private readonly PlaceOrderCommand command = new PlaceOrderCommand()
        {
            UserId = Guid.NewGuid(),
            ShippingAddress = "01 Nguyen Huu Tho, Da Nang, Viet Nam.",
            ShippingPhoneNumber = "000-000-0000"
        };
        
        [Fact]
        public async Task GetAnOrder_WhenPlacingOrder_ThenItShouldReturnCommandResultSuccess()
        {
            var cart = GivenSampleCart();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockOrderRepository.Setup(p => p.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var handler = new PlaceOrderCommandHandler(mockCartRepository.Object, mockOrderRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenThatNoCartFoundByUserId_WhenPlacingOrder_ThenItShouldReturnCommandResultError()
        {
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockOrderRepository.Setup(p => p.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var handler = new PlaceOrderCommandHandler(mockCartRepository.Object, mockOrderRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Do not find a cart with customer id.";
            Assert.Equal(message, result.Message);
        }

        private Cart GivenSampleCart()
        {
            var cartProductId = 1;
            var productId = 1;
            var productPrice = MoneyValue.Of(500, "USA");
            var quantity = 2;

            var cartProduct = new CartProduct(productId, quantity, productPrice);
            cartProduct.Id = cartProductId;
            var cart = new Cart(command.UserId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
