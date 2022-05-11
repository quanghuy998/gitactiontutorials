using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.RoleAggregate
{
    public class Role : AggregateRoot<Guid>
    {
        public string Name { get; }

        private Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
