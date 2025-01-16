using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;


namespace SharedLib.CQRS.CQRS_AOP
{
    public class LogAop<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse>
        where TRequest : ICommand<TReponse>
    {
        private readonly ILogger<LogAop<TRequest, TReponse>> logger;
        public LogAop(ILogger<LogAop<TRequest, TReponse>> logger) 
        {
            this.logger = logger;
        }
        public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
        {
            var command = JsonConvert.SerializeObject(request);
            logger.LogInformation($"命令开始！！！  命令请求参数:{command}");
            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            var commandResponse = JsonConvert.SerializeObject(response);
            logger.LogInformation($"命令结束！！！  命令耗时:{timeTaken.TotalSeconds} 返回值:{commandResponse}");
            return response;
        }
    }
}
