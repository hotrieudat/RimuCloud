using Mediator;
using RimuCloud.Application.Interfaces;
using RimuCloud.Shared.CustomResult;
using System.Collections.Concurrent;
using System.Reflection;

namespace RimuCloud.Application.Behaviors
{
#pragma warning disable CS8603, CS8602, CS8600 // Possible null reference return.
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IMessage
    where TResponse : Result
    {

        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;

        private static readonly ConcurrentDictionary<Type, Type> _requirementHandlers = new ConcurrentDictionary<Type, Type>();
        private static readonly ConcurrentDictionary<Type, MethodInfo> _handlerMethodInfo = new ConcurrentDictionary<Type, MethodInfo>();

        private IServiceProvider _serviceProvider;

        public RequestAuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers, IServiceProvider serviceProvider)
        {
            _authorizers = authorizers;
            _serviceProvider = serviceProvider;
        }

        public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
        {
            var requirements = new HashSet<IAuthorizationRequirement>();

            foreach (var authorizer in _authorizers)
            {
                if (!(authorizer.Requirements is null) && authorizer.Requirements.Any())
                    authorizer.ClearRequirements();

                authorizer.BuildPolicy(request);
                foreach (var requirement in authorizer.Requirements)
                    requirements.Add(requirement);
            }

            foreach (var requirement in requirements)
            {
                var result = await ExecuteAuthorizationHandler(requirement, cancellationToken);

                if (!result.IsAuthorized)
                    return CreateAuthorizationResult<TResponse>(result);
            }

            return await next(request, cancellationToken);
        }

        private static TResult CreateAuthorizationResult<TResult>(AuthorizationResult authorizationResult) where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))

                return (authorizationResult as TResult)!;

            var genericArgument = typeof(TResult).GetGenericArguments()[0];
            var failureResult = typeof(AuthorizationResult<>)
                .MakeGenericType(genericArgument)
                .GetMethod("Fail", new[] { typeof(Error) })!
                .Invoke(null, new object[] { authorizationResult.Error });

            return (TResult)failureResult!;
        }

        private Task<AuthorizationResult> ExecuteAuthorizationHandler(IAuthorizationRequirement requirement, CancellationToken cancellationToken)
        {
            var requirementType = requirement.GetType();
            var handlerType = FindHandlerType(requirement);

            if (handlerType == null)
                throw new InvalidOperationException($"Could not find an authorization handler type for requirement type \"{requirementType.Name}\"");

            var handlers = _serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as IEnumerable<object>;

            if (handlers == null || handlers.Count() == 0)
                throw new InvalidOperationException($"Could not find an authorization handler implementation for requirement type \"{requirementType.Name}\"");

            if (handlers.Count() > 1)
                throw new InvalidOperationException($"Multiple authorization handler implementations were found for requirement type \"{requirementType.Name}\"");

            var serviceHandler = handlers.First();
            var serviceHandlerType = serviceHandler.GetType();

            var methodInfo = _handlerMethodInfo.GetOrAdd(serviceHandlerType,
                handlerMethodKey =>
                {
                    return serviceHandlerType.GetMethods()
                        .Where(x => x.Name == nameof(IAuthorizationHandler<IAuthorizationRequirement>.Handle))
                        .FirstOrDefault();
                });

            return (Task<AuthorizationResult>)methodInfo.Invoke(serviceHandler, new object[] { requirement, cancellationToken });
        }

        private Type FindHandlerType(IAuthorizationRequirement requirement)
        {

            var requirementType = requirement.GetType();
            var handlerType = _requirementHandlers.GetOrAdd(requirementType,
                requirementTypeKey =>
                {
                    var wrapperType = typeof(IAuthorizationHandler<>).MakeGenericType(requirementTypeKey);

                    return wrapperType;
                });

            if (handlerType == null)
                return null;

            return handlerType;
        }
    }
#pragma warning restore CS8603, CS8602, CS8600
}