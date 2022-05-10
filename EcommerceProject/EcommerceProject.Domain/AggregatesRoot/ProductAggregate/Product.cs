using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.Domain.AggregatesRoot.ProductAggregate
{
    public class Product : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public MoneyValue Price { get; private set; }
        public string TradeMark { get; private set; }
        public string Origin { get; private set; }
        public string Discription { get; private set; }

        private Product()
        {
        }

        public Product(string name, MoneyValue price, string tradeMark, string origin, string discription)
        {
            this.Name = name;
            this.Price = price;
            this.TradeMark = tradeMark;
            this.Origin = origin;
            this.Discription = discription;
        }

        public void UpdateProduct(string name, MoneyValue price, string tradeMark, string origin, string discription)
        {
            this.Name = name;
            this.Price = price;
            this.TradeMark = tradeMark;
            this.Origin = origin;
            this.Discription = discription;
        }
    }
}
