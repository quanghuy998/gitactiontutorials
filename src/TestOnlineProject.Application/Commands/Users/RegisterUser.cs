using TestOnlineProject.Domain.Aggregates.RoleAggregate;
using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Users
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
    }

    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public RegisterUserHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<User>(x => x.UserName == request.UserName);
            var result = await _userRepository.ExistsAsync(spec, cancellationToken);
            if (result) return CommandResult.Error("User name already exists.");

            var specification = new BaseSpecification<Role>(x => x.Name == "Candidate");
            var role = await _roleRepository.FindOneAsync(specification, cancellationToken);

            var user = new User(request.Name, request.UserName, request.Email, role);
            await _userRepository.AddAsync(user, cancellationToken);

            return CommandResult.Success(); 
        }
    }
}
