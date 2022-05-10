using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.API.Dtos
{
    public class GetProductsRequest
    {
        public string Name { get; set; }
        public string TradeMark { get; set; }
        public string Origin { get; set; }
        public string Currency { get; set; }
        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
    }
}
