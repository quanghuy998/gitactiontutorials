using TestOnlineProject.Domain.Aggregates.RoleAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.UserAggregate
{
    public class User : AggregateRoot<Guid>
    {
        public string Name { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }
        public Role Role { get; }

        private User()
        {

        }

        public User(string name, string userName, string email, Role role)
        {
            Name = name;
            UserName = userName;
            Email = email;
            Role = role;
        }
    }
}
