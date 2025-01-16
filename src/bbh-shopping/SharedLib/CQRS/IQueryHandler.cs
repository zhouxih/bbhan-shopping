using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.CQRS
{
    public interface IQueryHandler<IQeury> : IRequestHandler<IQeury>
        where IQeury : IQuery
    {

    }


    public interface IQueryHandler<IQeury, TResponse> : IRequestHandler<IQeury, TResponse>
        where IQeury : IQuery<TResponse>
    {

    }
}
