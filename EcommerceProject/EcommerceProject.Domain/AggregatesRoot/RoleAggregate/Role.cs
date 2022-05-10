using EcommerceProject.Domain.SeedWork;

namespace EcommerceProject.Domain.AggregatesRoot.RoleAggregate
{
    public class Role : AggregateRoot<int>
    {
        public string Name { get; }

        private Role()
        {
        }

        public Role(string name)
        {
            this.Name = name;
        }
    }
}
