namespace EcommerceProject.API.Dtos
{
    public class ChangeProductQuantityRequest
    {
        public int CartProductId { get; init; }
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
