using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.Domain.AggregatesRoot.CartAggregate
{
    public class Cart : AggregateRoot<int>
    {
        public Guid UserId { get; }
        public MoneyValue Value { get; private set; }
        public List<CartProduct> CartProducts { get; private set; }

        private Cart()
        {
        }

        public Cart(Guid userId)
        {
            this.UserId = userId;
            this.Value = null;
            this.CartProducts = new List<CartProduct>();
        }

        public void AddCartProduct(CartProduct cartProduct)
        {
            this.CartProducts.Add(cartProduct);
            CalculateCartValue();
        }

        public void ChangeCartProductQuantity(int orderProductId, int quantity)
        {
            var cartProduct = CartProducts.Find(x => x.Id == orderProductId);
            cartProduct.ChangeQuantity(quantity);
            CalculateCartValue();
        }

        public void RemoveCartProduct(int orderProductId)
        {
            var cartProduct = CartProducts.Find(x => x.Id == orderProductId);
            this.CartProducts.Remove(cartProduct);
            if (this.CartProducts.Count == 0) this.Value = null;
            else CalculateCartValue();
        }

        public void RemoveAllCartProduct()
        {
            this.CartProducts = new List<CartProduct>();
            this.Value = null;
        }
        private void CalculateCartValue()
        {
            this.Value = this.CartProducts.Sum(x => x.Quantity * x.Price);
        }
    }
}
