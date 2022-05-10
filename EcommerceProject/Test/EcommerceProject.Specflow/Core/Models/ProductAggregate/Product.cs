namespace EcommerceProject.Specflow.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MoneyValue Price { get; set; }
        public string TradeMark { get; set; }
        public string Origin { get; set; }
        public string Discription { get; set; }

        public Product(string name, MoneyValue price, string tradeMark, string origin, string discription)
        {
            this.Name = name;
            this.Price = price;
            this.TradeMark = tradeMark;
            this.Origin = origin;
            this.Discription = discription;
        }
    }
}
