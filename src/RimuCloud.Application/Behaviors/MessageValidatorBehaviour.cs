using Mediator;
using RimuCloud.Application.Abstractions;
using RimuCloud.Domain.Exceptions;

namespace RimuCloud.Application.Behaviors
{
    public sealed class MessageValidatorBehaviour<TMessage, TResponse> : MessagePreProcessor<TMessage, TResponse>
    where TMessage : IValidate
    {
        protected override ValueTask Handle(TMessage message, CancellationToken cancellationToken)
        {
            if (!message.IsValid(out var validationError))
                throw new ValidationException(validationError);

            return default;
        }
    }
}
