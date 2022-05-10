using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.API.Dtos
{
    public class UpdateProductRequest
    {
        public string Name { get; init; }
        public string Currency { get; init; }
        public decimal Value { get; init; }
        public string TradeMark { get; init; }
        public string Origin { get; init; }
        public string Discription { get; init; }
    }
}
