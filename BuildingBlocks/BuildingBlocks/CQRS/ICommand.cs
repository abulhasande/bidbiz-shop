using MediatR;

namespace BuildingBlocks.CQRS
{
    //public interface ICommand : ICommandHandler<Unit>
    //{

    //}

    //public interface ICommandHandler<out TResponse> : IRequest<TResponse>
    //{
    //}

    public interface ICommand : ICommand<Unit>
    {

    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}

