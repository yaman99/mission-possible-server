using MissionPossible.Application.Common.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<FluentValidation.IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<FluentValidation.IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, 
                                            CancellationToken cancellationToken, 
                                            RequestHandlerDelegate<TResponse> next)
        {

            if (!_validators.Any())
            {
                return await next();
            }
            var failures = _validators
                            .Select(validator => validator.Validate(request))
                            .SelectMany(result => result.Errors)
                            .Where(error => error != null)
                            .ToList();
            //var failures = (await Task.whenAll(_validators
            //                .Select(validator => validator.ValidateAsync(request) )))
            //                .SelectMany(result => result.Errors)
            //                .Where(error => error != null)
            //                .ToList();

            if (failures.Any())
            {
                throw new SystemValidationException(failures);
            }

            return await next();
        }
    }
}
