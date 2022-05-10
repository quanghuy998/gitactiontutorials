namespace EcommerceProject.Infrastructure.CQRS.Command
{
    public interface ICommandBus
    {
        Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default);

        Task<CommandResult<TResponse>> SendAsyns<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default); 
    }
}
