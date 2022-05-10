using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.Domain.AggregatesRoot.OrderAggregate
{
    public class OrderProduct : Entity<int>
    {
        public int ProductId { get; }
        public int Quantity { get; }
        public MoneyValue Price { get; }

        private OrderProduct()
        {
        }

        public OrderProduct(int productId, int quantity, MoneyValue price)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
