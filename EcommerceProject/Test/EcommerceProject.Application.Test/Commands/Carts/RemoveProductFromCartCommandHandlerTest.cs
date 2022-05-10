using EcommerceProject.Application.Commands.Carts.RemoveProductFromCart;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Application.Test.Commands.Carts
{
    public class RemoveProductFromCartCommandHandlerTest
    {
        private readonly Mock<ICartRepository> mockCartRepository = new Mock<ICartRepository>();
        private readonly RemoveProductFromCartCommand command = new RemoveProductFromCartCommand()
        {
            UserId = Guid.NewGuid(),
            CartProductId = 1
        };

        [Fact]
        public async Task GetACart_WhenRemovingProductFromCart_ThenItShouldReturnCommandResultSuccess()
        {
            var cart = GivenSampleCart();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var handler = new RemoveProductFromCartCommandHandler(mockCartRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenThatNoCartFoundByUserId_WhenRemovingProductFromCart_ThenItShouldReturnCommandResultError()
        {
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var handler = new RemoveProductFromCartCommandHandler(mockCartRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Do not find a cart with customer id.";
            Assert.Equal(message, result.Message);
        }

        private Cart GivenSampleCart()
        {
            var productId = 1;
            var quantity = 3;
            var productPrice = MoneyValue.Of(100, "USA");
            var cartProduct = new CartProduct(productId, quantity, productPrice);
            cartProduct.Id = command.CartProductId;
            var cart = new Cart(command.UserId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
