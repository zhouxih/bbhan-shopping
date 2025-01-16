using FluentValidation;
using MediatR;


namespace SharedLib.CQRS.CQRS_AOP
{
    public class ValidateAop<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> validators;
        public ValidateAop(IEnumerable<IValidator<TRequest>> validators) 
        {
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var allValidateTasks = validators.Select(v => v.ValidateAsync(request));
                var results = await Task.WhenAll(allValidateTasks);
                var failures =
                    results
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();
                if (failures.Any()) 
                {
                    throw new FluentValidation.ValidationException(failures);
                }

            }
            return await next();
        }
    }
}
