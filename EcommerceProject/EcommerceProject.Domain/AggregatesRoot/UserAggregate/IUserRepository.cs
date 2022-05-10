using EcommerceProject.Domain.SeedWork;

namespace EcommerceProject.Domain.AggregatesRoot.UserAggregate
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
    }
}
