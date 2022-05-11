using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Users
{
    public class GetUserDetailQuery : IQuery<User>
    {
        public Guid UserId { get; init; }
    }

    public class GetUserDetailHandler : IQueryHandler<GetUserDetailQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserDetailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.FindOneAsync(request.UserId, cancellationToken);
        }
    }
}
