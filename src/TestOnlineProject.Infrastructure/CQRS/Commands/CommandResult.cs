namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        protected CommandResult()
        {
        }

        public static CommandResult Success()
        {
            return new CommandResult { IsSuccess = true };
        }

        public static CommandResult Error(string message = null)
        {
            return new CommandResult { Message = message };
        }
    }
}
