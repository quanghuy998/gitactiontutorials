using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.Application.Queries.Users.GetCustomers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _customerRepository;

        public GetUsersQueryHandler(IUserRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<User>(x => 1 == 1);
            spec.Includes.Add(x => x.Role);
            return await _customerRepository.FindAllAsync(spec, cancellationToken);
        }
    }
}
