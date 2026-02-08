using FluentValidation;
using Mediator;
using RimuCloud.Shared.CustomResult;

namespace RimuCloud.Application.Behaviors
{
    public sealed class ValidationDefaultBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMessage
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationDefaultBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next(request, cancellationToken);

            Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

            if (errors.Any())
            {
                var cxc = CreateValidationResult<TResponse>(errors);
                return cxc;
            }

            return await next(request, cancellationToken);
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))

                return (ValidationResult.WithErrors(errors) as TResult)!;

            object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

            return (TResult)validationResult;
        }
    }
}
