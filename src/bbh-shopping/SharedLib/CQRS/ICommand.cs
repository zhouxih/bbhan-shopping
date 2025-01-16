using MediatR;

namespace SharedLib.CQRS
{
    /// <summary>
    /// 标识这个是命令
    /// </summary>

    public interface ICommand : IRequest
    {

    }

    /// <summary>
    ///  标识这个是命令
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}
