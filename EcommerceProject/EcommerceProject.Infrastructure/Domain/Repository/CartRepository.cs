using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.Database;

namespace EcommerceProject.Infrastructure.Domain.Repository
{
    internal class CartRepository : BaseRepository<Cart, int>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
        Task<Cart> IBaseRepository<Cart, int>.FindOneAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<Cart>(x => x.Id == id);
            spec.Includes.Add(x => x.CartProducts);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}
