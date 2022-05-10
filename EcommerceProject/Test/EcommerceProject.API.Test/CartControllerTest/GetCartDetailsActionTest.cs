using EcommerceProject.API.Controllers;
using EcommerceProject.Application.Queries.Carts.GetCartDetails;
using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.SharedKermel;
using EcommerceProject.Infrastructure.CQRS.Command;
using EcommerceProject.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.API.Test.CartControllerTest
{
    public class GetCartDetailsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new Mock<IQueryBus>();
        private readonly Mock<ICommandBus> mockCommandBus = new Mock<ICommandBus>();
        private Guid customerId = Guid.NewGuid();

        [Fact]
        public async Task GivenInformation_WhenGettingCartDetails_ThenItShouldReturnOk()
        {
            var cart = GivenSampleCart();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetCartDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(cart);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetCartDetails(customerId, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var cartRepsonse = okResult.Value as Cart;

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(cart.UserId, cartRepsonse.UserId);
            Assert.Equal(cart.Value, cartRepsonse.Value);
            Assert.Equal(cart.CartProducts.Count, cartRepsonse.CartProducts.Count);
            Assert.Equal(cart.CartProducts[0].ProductId, cartRepsonse.CartProducts[0].ProductId);
            Assert.Equal(cart.CartProducts[0].Quantity, cartRepsonse.CartProducts[0].Quantity);
            Assert.Equal(cart.CartProducts[0].Price, cartRepsonse.CartProducts[0].Price);
        }

        [Fact]
        public async Task GivenThatNotFoundCustomer_WhenGettingCartDetails_ThenItShouldBeReturnNotFound()
        {
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetCartDetailsQuery>(), CancellationToken.None)).ReturnsAsync(() => null);
            var controller = new CartController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetCartDetails(customerId, CancellationToken.None);
            var notFoundResult = actionResult as NotFoundObjectResult;

            string value = "Customer is not exist.";
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal(value, notFoundResult.Value);
        }


        private Cart GivenSampleCart()
        {
            var cartProductId = 1;
            var productId = 1;
            var quantity = 2;
            var productPrice = MoneyValue.Of(100, "USA");
            var cartProduct = new CartProduct(productId, quantity, productPrice);
            cartProduct.Id = cartProductId;
            var cart = new Cart(customerId);
            cart.AddCartProduct(cartProduct);

            return cart;
        }
    }
}
