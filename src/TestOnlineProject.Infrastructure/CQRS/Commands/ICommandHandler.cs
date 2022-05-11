using MediatR;

namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult>
        where TCommand : IRequest<CommandResult>
    {

    }
}
