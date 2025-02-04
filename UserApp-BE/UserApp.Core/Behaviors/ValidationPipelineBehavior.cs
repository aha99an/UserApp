using ErrorOr;
using FluentValidation;
using MediatR;

namespace UserApp.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var errors = Validate(new ValidationContext<TRequest>(request));

            if (errors.Length != 0)
            {
                return (dynamic)errors;
            }

            return await next();
        }

        private Error[] Validate(IValidationContext validationContext) =>
        _validators.Select(validator => validator.Validate(validationContext))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(validationFailure => Error.Validation(
                    code: validationFailure.PropertyName,
                    description: validationFailure.ErrorMessage))
            .Distinct()
            .ToArray();
    }

}
