using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.UserAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Users
{
    public class DeleteUserCommand : ICommand
    {
        public Guid UserId { get; init; }
    }

    public class UpdateUserHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.FindOneAsync(request.UserId, cancellationToken);
            if (result == null) return CommandResult.Error("Customer is not exist.");
            await _userRepository.DeleteAsync(result, cancellationToken);

            return CommandResult.Success();
        }
    }
}
