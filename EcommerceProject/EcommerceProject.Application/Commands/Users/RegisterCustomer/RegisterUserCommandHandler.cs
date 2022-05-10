using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Users.RegisterCustomer
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserData>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IRoleRepository _roleRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository, ICartRepository cartRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CommandResult<UserData>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<User>(x => x.UserName == command.UserName);
            var user = await _userRepository.FindOneAsync(spec, cancellationToken);
            if (user != null) return CommandResult<UserData>.Error("User Name already exists.");

            var specRole = new SpecificationBase<Role>(x => x.Name == "Customer");
            var role = await _roleRepository.FindOneAsync(specRole, cancellationToken);
            if (role == null)
            {
                var newrole = new Role("Customer");
                await _roleRepository.AddAsync(newrole);
                role = newrole;
            }

            var customer = new User(command.UserName, command.Password, command.Name, command.Email, role);
            await _userRepository.AddAsync(customer, cancellationToken);

            var cart = new Cart(customer.Id);
            await _cartRepository.AddAsync(cart, cancellationToken);
            var customerData = new UserData(customer.Id, cart.Id);

            return CommandResult<UserData>.Success(customerData);
        }
    }
}
