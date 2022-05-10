using EcommerceProject.Application.Commands.Carts.AddProductToCart;
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
    public class AddProductToCartCommandHandlerTest
    {
        private Mock<ICartRepository> mockCartRepository = new Mock<ICartRepository>();
        private Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private AddProductToCartCommand command = new AddProductToCartCommand()
        {
            ProductId = 1,
            Quantity = 1,
            UserId = Guid.NewGuid(),
        };

        [Fact]
        public async Task GivenACart_WhenAddingProductToCart_ThenItShouldBeReturnCommandResultSuccess()
        {
            var cart = GivenSampleCart();
            var product = GivenSampleProduct();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var handler = new AddProductToCartCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenThatNoCartFoundByUserId_WhenAddingProductToCart_ThenItShouldReturnCommandResultError()
        {
            var product = GivenSampleProduct();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var handler = new AddProductToCartCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Do not find a cart with customer id.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenThatNoProductFoundByProductId_WhenAddingProductToCart_ThenItShouldReturnCommandResultError()
        {
            var cart = GivenSampleCart();
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            mockCartRepository.Setup(p => p.SaveAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockProductRepository.Setup(p => p.FindOneAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new AddProductToCartCommandHandler(mockCartRepository.Object, mockProductRepository.Object);

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
            var cartProductId = 1;
            var product = GivenSampleProduct();
            var cartProduct = new CartProduct(command.ProductId, command.Quantity, product.Price);
            cartProduct.Id = cartProductId;
            var cart = new Cart(command.UserId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
