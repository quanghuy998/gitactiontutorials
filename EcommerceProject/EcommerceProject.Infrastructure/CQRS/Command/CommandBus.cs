using EcommerceProject.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceProject.Infrastructure.CQRS.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.ExecuteAsync(() => _mediator.Send(command, cancellationToken), cancellationToken);
        }

        public Task<CommandResult<TResponse>> SendAsyns<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.ExecuteAsync(() => _mediator.Send(command, cancellationToken), cancellationToken);
        }
    }
}
