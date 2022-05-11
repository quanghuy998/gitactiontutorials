namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public interface ICommandBus
    {
        Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
