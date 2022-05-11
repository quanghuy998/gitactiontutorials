using MediatR;

namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public interface ICommand : IRequest<CommandResult>
    {

    }
}
