using EcommerceProject.Application.Commands.Carts.ChangeProductQuantity;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Application.Test.Commands.Carts
{
    public class ChangeProductQuantityCommandHandlerTest
    {
        private readonly Mock<ICartRepository> mockCartRepository = new Mock<ICartRepository>();
        private readonly Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private readonly ChangeProductQuantityCommand command = new ChangeProductQuantityCommand()
        {
            UserId = Guid.NewGuid(),
            ProductId = 1,
            CartProductId = 1,
            Quantity = 3,
        };

        [Fact]
        public async Task GivenACart_WhenChangingProductQuantityToCart_ThenItShouldReturnCommandResultSuccess()
        {
            var product = GivenSampleProduct();
            var cart = GivenSampleCart();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var handler = new ChangeProductQuantityCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenThatNoCartFoundByUserId_WhenChangingProductQuantity_ThenItShouldReturnCommandResultError()
        {
            var product = GivenSampleProduct();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var handler = new ChangeProductQuantityCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string messsage = "Do not find a cart with customer id.";
            Assert.Equal(messsage, result.Message);
        }

        [Fact]
        public async Task GivenThatNoProductFoundByProductId_WhenChagingProductQuantity_ThenItShouldReturnCommandResultError()
        {
            var cart = GivenSampleCart();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new ChangeProductQuantityCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Your product is not exist.";
            Assert.Equal(message, result.Message);
        }

        private Product GivenSampleProduct()
        {
            var name = "Macbook";
            var price = MoneyValue.Of(500, "USA");
            var tradeMark = "Apple";
            var origin = "China";
            var discription = "This is a macbook.";
            var product = new Product(name, price, tradeMark, origin, discription);
            product.Id = command.ProductId;
            return product;
        }

        private Cart GivenSampleCart()
        {
            var product = GivenSampleProduct();
            var cartProduct = new CartProduct(command.ProductId, command.Quantity, product.Price);
            cartProduct.Id = command.CartProductId;
            var cart = new Cart(command.UserId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
