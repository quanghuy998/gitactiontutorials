using MediatR;

namespace EcommerceProject.Infrastructure.CQRS.Command
{
    public interface ICommand : IRequest<CommandResult>
    {

    }

    public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
    {

    }
}
