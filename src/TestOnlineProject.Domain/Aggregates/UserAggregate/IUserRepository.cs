using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.UserAggregate
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
    }
}
