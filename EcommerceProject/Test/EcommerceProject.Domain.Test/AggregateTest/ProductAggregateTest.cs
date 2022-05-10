using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.SharedKermel;
using Xunit;

namespace EcommerceProject.Domain.Test.AggregateTest
{
    public class ProductAggregateTest
    {
        public void GivenInfomation_WhenCreatingProduct_ThenItShouldBeCreated() 
        {
            var name = "Macbook";
            var price = MoneyValue.Of(1000, "USA");
            var tradeMark = "Apple";
            var origin = "China";
            var discription = "This is a macbook";

            var product = new Product(name, price, tradeMark, origin, discription);

            Assert.Equal(name, product.Name);
            Assert.Equal(price.Value, product.Price.Value);
            Assert.Equal(price.Currency, product.Price.Currency);
            Assert.Equal(tradeMark, product.TradeMark);
            Assert.Equal(origin, product.Origin);
            Assert.Equal(discription, product.Discription);
        }
        public void GivenInfomation_WhenUpdatingProduct_ThenItShouldBeUpdated() 
        {
            var product = GivenSampleProduct();
            var name = "Product 2";
            var price = MoneyValue.Of(200, "VND");
            var tradeMark = "VietNam";
            var origin = "China";
            var discription = "This is not a product 1.";

            product.UpdateProduct(name, price, tradeMark, origin, discription);

            Assert.Equal(name, product.Name);
            Assert.Equal(price.Value, product.Price.Value);
            Assert.Equal(price.Currency, product.Price.Currency);
            Assert.Equal(tradeMark, product.TradeMark);
            Assert.Equal(origin, product.Origin);
            Assert.Equal(discription, product.Discription);
        }
        private Product GivenSampleProduct()
        {
            var name = "Product 1";
            var price = MoneyValue.Of(100, "USA");
            var tradeMark = "China";
            var origin = "VietNam";
            var discription = "This is a product.";

            return new Product(name, price, tradeMark, origin, discription);
        }
    }
}
