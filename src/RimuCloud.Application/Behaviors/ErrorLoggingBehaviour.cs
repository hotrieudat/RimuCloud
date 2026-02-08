using Mediator;
using RimuCloud.Abstractions.Logger;


namespace RimuCloud.Application.Behaviors
{
    public sealed class ErrorLoggingBehaviour<TMessage, TResponse> : MessageExceptionHandler<TMessage, TResponse>
    where TMessage : notnull, IMessage
    {
        private readonly ILoggerManager _logger;

        public ErrorLoggingBehaviour(ILoggerManager logger)
        {
            _logger = logger;
        }

        protected override ValueTask<ExceptionHandlingResult<TResponse>> Handle(
            TMessage message,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            _logger.Error(exception, "Error handling message of type {messageType}", message.GetType().Name);
            return NotHandled;
        }
    }
}
