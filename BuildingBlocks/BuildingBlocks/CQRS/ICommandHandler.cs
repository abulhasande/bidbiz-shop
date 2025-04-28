using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHanlder<in TCommand>
                       : ICommandHandler<TCommand, Unit>
                        where TCommand : ICommandHandler<Unit>
    {

    }
    public interface ICommandHandler<in TCommand, TResponse>
                       : IRequestHandler<TCommand, TResponse>
                            where TCommand : ICommandHandler<TResponse>
                            where TResponse : notnull
    {
    }
}
