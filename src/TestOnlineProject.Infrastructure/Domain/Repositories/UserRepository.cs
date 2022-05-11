using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        Task<User> IRepository<User, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<User>(x => x.Id == id);
            spec.Includes.Add(x => x.Role);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}


