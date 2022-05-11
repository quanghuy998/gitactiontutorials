using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Users
{
    public class GetAllUserQuery : IQuery<IEnumerable<User>>
    {

    }

    public class GetAllUserHander : IQueryHandler<GetAllUserQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserHander(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.FindAllAsync(null, cancellationToken);
        }
    }
}
