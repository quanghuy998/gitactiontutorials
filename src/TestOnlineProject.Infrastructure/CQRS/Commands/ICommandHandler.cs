using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult>
        where TCommand : IRequest<CommandResult>
    {

    }
}
