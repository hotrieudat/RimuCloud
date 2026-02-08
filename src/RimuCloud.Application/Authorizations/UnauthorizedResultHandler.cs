using RimuCloud.Application.Interfaces;
using RimuCloud.Shared.CustomResult;
#nullable disable
namespace RimuCloud.Application.Common.Authorizations
{
    public class UnauthorizedResultHandler : IUnauthorizedResultHandler
    {
        public Task<TResponse> Invoke<TResponse>(AuthorizationResult result)
        {
            return Task.FromResult(default(TResponse));
            //return Task.FromResult(CreateAuthorizationResult<TResponse>(result));
        }

        //private static TResult CreateAuthorizationResult<TResult>(AuthorizationResult errors)
        //where TResult : Result
        //{
        //    if (typeof(TResult) == typeof(Result))
        //    {
        //        Error[] aaaa = new[] { new Error("Authorization Fail", errors.FailureMessage) };
        //        return (AuthorizerResultFail.WithErrors(aaaa) as TResult)!;
        //    }

        //    object validationResult = typeof(AuthorizerResultFail<>)
        //    .GetGenericTypeDefinition()
        //    .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
        //    .GetMethod(nameof(AuthorizerResultFail.WithErrors))!
        //    .Invoke(null, new object?[] { errors })!;

        //    return (TResult)validationResult;
        //}
    }
}
