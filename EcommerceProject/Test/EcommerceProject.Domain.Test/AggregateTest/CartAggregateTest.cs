using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.SharedKermel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Domain.Test.AggregateTest
{
    public class CartAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingACart_ThenItShouldBeCreated()
        {
            var userId = Guid.NewGuid();

            var cart = new Cart(userId);

            Assert.Equal(userId, cart.UserId);
            Assert.Null(cart.Value);
            Assert.Empty(cart.CartProducts);
        }

        [Fact]
        public void GivenInformation_WhenCreatingCartProduct_ThenItShouldBeCreated()
        {
            var productId = 1;
            var price = MoneyValue.Of(100, "USA");
            var quantity = 2;

            var cartProduct = new CartProduct(productId, quantity, price);

            Assert.Equal(productId, cartProduct.ProductId);
            Assert.Equal(price, cartProduct.Price);
            Assert.Equal(quantity, cartProduct.Quantity);
        }

        [Fact]
        public void GivenMoneyValue_WhenCalculatingTheValueOfCart_ThenItShouldBeExactly()
        {

            var cartProduct1 = GivenSampleCartProduct1();
            var cartProduct2 = GivenSampleCartProduct2();
            var cart = GivenSampleCart();

            cart.AddCartProduct(cartProduct1);
            cart.AddCartProduct(cartProduct2);

            var result = cartProduct1.Quantity * cartProduct1.Price + cartProduct2.Quantity * cartProduct2.Price;
            Assert.Equal(result, cart.Value);
        }

        [Fact]
        public void GivenACart_WhenAddingCartProductToCart_ThenInShoudldBeAdd()
        {
            var cartProduct1 = GivenSampleCartProduct1();
            var cartProduct2 = GivenSampleCartProduct2();
            var cart = GivenSampleCart();

            cart.AddCartProduct(cartProduct1);
            cart.AddCartProduct(cartProduct2);

            Assert.Equal(2, cart.CartProducts.Count);
            Assert.Equal(cartProduct1.ProductId, cart.CartProducts[0].ProductId);
            Assert.Equal(cartProduct1.Price, cart.CartProducts[0].Price);
            Assert.Equal(cartProduct1.Quantity, cart.CartProducts[0].Quantity);
            Assert.Equal(cartProduct2.ProductId, cart.CartProducts[1].ProductId);
            Assert.Equal(cartProduct2.Price, cart.CartProducts[1].Price);
            Assert.Equal(cartProduct2.Quantity, cart.CartProducts[1].Quantity);
        }

        [Fact]
        public void GivenACart_WhenChangingQuantityOfCartProduct_ThenItShouldBeChangedAndReCalculatedValue()
        {
            var cartProduct = GivenSampleCartProduct1();
            var cart = GivenSampleCart();
            var quantityChanged = 2;

            cart.AddCartProduct(cartProduct);
            cart.ChangeCartProductQuantity(cartProduct.Id, quantityChanged);

            var valueChanged = quantityChanged * cartProduct.Price;
            Assert.Equal(quantityChanged, cart.CartProducts[0].Quantity);
            Assert.Equal(valueChanged, cart.Value);
        }

        [Fact]
        public void GivenACart_WhenRemovingCartProduct_ThenItShouldBeRemoved()
        {
            var cartProduct1 = GivenSampleCartProduct1();
            var cart = GivenSampleCart();

            cart.AddCartProduct(cartProduct1);
            cart.RemoveCartProduct(cartProduct1.Id);

            Assert.Empty(cart.CartProducts);
        }

        [Fact]
        public void GivenACart_WhenRemovingAllCartProduct_ThenItShouldBeEmpty()
        {
            var cartProduct1 = GivenSampleCartProduct1();
            var cartProduct2 = GivenSampleCartProduct2();
            var cart = GivenSampleCart();

            cart.AddCartProduct(cartProduct1);
            cart.AddCartProduct(cartProduct2);
            cart.RemoveAllCartProduct();

            Assert.Empty(cart.CartProducts);
        }

        private Cart GivenSampleCart()
        {
            var userId = Guid.NewGuid();
            return new Cart(userId);
        }

        private CartProduct GivenSampleCartProduct1()
        {
            var productId = 1;
            var price = MoneyValue.Of(15, "USA");
            var quantity = 3;

            return new CartProduct(productId, quantity, price);
        }

        private CartProduct GivenSampleCartProduct2()
        {
            var productId = 2;
            var price = MoneyValue.Of(20, "USA");
            var quantity = 5;

            return new CartProduct(productId, quantity, price);
        }
    }
}
