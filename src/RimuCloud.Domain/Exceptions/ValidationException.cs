using RimuCloud.Domain.Entities.ErrorModel;

namespace RimuCloud.Domain.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(ValidationError validationError)
            : base("Validation error") => ValidationError = validationError;

        public ValidationError ValidationError { get; }
    }
}
