using Mediator;

namespace RimuCloud.Application.Behaviors
{
    public sealed class TestBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
    {
        //public ValueTask<TResponse> Handle(
        //    TMessage message,
        //    CancellationToken cancellationToken,
        //    MessageHandlerDelegate<TMessage, TResponse> next
        //)
        //{
        //    if (!message.IsValid(out var validationError))
        //        throw new ValidationException(validationError);

        //    return next(message, cancellationToken);
        //}
        public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next(message, cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
