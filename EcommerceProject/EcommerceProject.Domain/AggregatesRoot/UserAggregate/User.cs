using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using EcommerceProject.Domain.SeedWork;

namespace EcommerceProject.Domain.AggregatesRoot.UserAggregate
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

        public User(string userName, string passWord, string name, string email, Role role)
        {
            this.UserName = userName;
            this.Password = passWord;
            this.Name = name;
            this.Email = email;
            this.Role = role;
        }
    }
}
