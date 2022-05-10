using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.API.Dtos
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal MoneyValue { get; set; }
        public string Currency { get; set; }
        public string TradeMark { get; set; }
        public string Origin { get; set; }
        public string Discription { get; set; }
    }
}
