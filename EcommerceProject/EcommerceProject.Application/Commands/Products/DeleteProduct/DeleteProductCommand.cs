using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Products.DeleteProduct
{
    public class DeleteProductCommand : ICommand<int>
    {
        public int ProductId { get; init; }
    }
}
