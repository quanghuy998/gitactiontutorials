using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.Domain.AggregatesRoot.CartAggregate
{
    public class CartProduct : Entity<int>
    {
        public int ProductId { get; }
        public MoneyValue Price { get; }
        public int Quantity { get; private set; }

        private CartProduct()
        {
        }

        public CartProduct(int productId, int quantity, MoneyValue price)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
        }

        public void ChangeQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}
