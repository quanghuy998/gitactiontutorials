using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.Database;

namespace EcommerceProject.Infrastructure.Domain.Repository
{
    internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        Task<User> IBaseRepository<User, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<User>(x => x.Id == id);
            spec.Includes.Add(x => x.Role);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}
