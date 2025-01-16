using MediatR;

namespace SharedLib.CQRS
{
    public interface ICommandHandler<ICommand> : IRequestHandler<ICommand>
         where ICommand : SharedLib.CQRS.ICommand
    {

    }


    public interface ICommandHandler<ICommand, TResponse> : IRequestHandler<ICommand, TResponse>
        where ICommand : ICommand<TResponse>
    {

    }
}
