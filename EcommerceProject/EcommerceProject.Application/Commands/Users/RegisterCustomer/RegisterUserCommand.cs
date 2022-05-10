using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Users.RegisterCustomer
{
    public class RegisterUserCommand : ICommand<UserData>
    {
        public string UserName { get ; init ; }
        public string Password { get; init; }
        public string Name { get; init; }
        public string Email { get ; init ; }
    }
}
