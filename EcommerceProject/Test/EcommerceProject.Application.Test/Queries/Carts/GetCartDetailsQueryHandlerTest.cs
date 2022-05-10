using EcommerceProject.Application.Queries.Carts.GetCartDetails;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Application.Test.Queries.Carts
{
    public class GetCartDetailsQueryHandlerTest
    {
        private readonly Mock<ICartRepository> mockCartRepository = new Mock<ICartRepository>();
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly GetCartDetailsQuery query = new GetCartDetailsQuery()
        {
            UserId = Guid.NewGuid()
        };
        
        [Fact]
        public async Task GivenACustomerCartDetails_WhenGettingCartDetails_ThenItShouldBeReturnData()
        {
            var user = GivenSampleUser();
            var cart = GivenSampleCart();
            mockUserRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            var handler = new GetCartDetailsQueryHandler(mockCartRepository.Object, mockUserRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(cart.Value, result.Value);
            Assert.Equal(cart.CartProducts[0].ProductId, result.CartProducts[0].ProductId);
            Assert.Equal(cart.CartProducts[0].Quantity, result.CartProducts[0].Quantity);
            Assert.Equal(cart.CartProducts[0].Price, result.CartProducts[0].Price);
        }

        [Fact]
        public async Task GivenThatNoCustomerFound_WhenGettingCartDetails_ThenItShouldBeReturnNull()
        {
            var cart = GivenSampleCart();
            mockUserRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            var handler = new GetCartDetailsQueryHandler(mockCartRepository.Object, mockUserRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GivenThatCustomerDoesNotExistAnyCart_WhenGettingCartDetails_ThenItShouldThrowCustomerMustHaveOnlyOneCartException()
        {
            var user = GivenSampleUser();
            mockUserRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            mockCartRepository.Setup(p => p.FindOneAsync(It.IsAny<SpecificationBase<Cart>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetCartDetailsQueryHandler(mockCartRepository.Object, mockUserRepository.Object);

            var exception = await Assert.ThrowsAsync<CustomerMustHaveOnlyOneCartException>(() => handler.Handle(query, CancellationToken.None));

            string message = "Each customer must has an cart. Something is broken.";
            Assert.Equal(message, exception.Message);
        }

        private User GivenSampleUser()
        {
            var userName = "admin";
            var password = "Abc@123";
            var name = "Admin";
            var email = "admin@mail.com";
            var role = new Role(UserRole.Customer);
            
            return new User(userName, password, name, email, role);
        }

        private Cart GivenSampleCart()
        {
            var cartProductId = 1;
            var productId = 1;
            var quantity = 3;
            var productPrice = MoneyValue.Of(100, "USA");
            var cartProduct = new CartProduct(productId, quantity, productPrice);
            cartProduct.Id = cartProductId;
            var cart = new Cart(query.UserId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
