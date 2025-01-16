using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.CQRS
{
    /// <summary>
    /// 标识这个是查询
    /// </summary>

    public interface IQuery : IRequest
    {

    }

    /// <summary>
    /// 标识这个是查询
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}
