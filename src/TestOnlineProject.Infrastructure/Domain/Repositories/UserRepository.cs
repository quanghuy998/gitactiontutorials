using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
    }
}
