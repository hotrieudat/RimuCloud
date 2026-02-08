using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RimuCloud.Shared.CustomResult;

namespace RimuCloud.ApiService.Presentation
{
    public abstract class AbstractEndpoint
    {
        protected AbstractEndpoint() : base() { }
        protected static IResult HandlerFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IAuthorizationResult authorizationResult => Results.Problem(CreateProblemDetails("Authorization Error", StatusCodes.Status401Unauthorized, result.Error)),
            IValidationResult validationResult => Results.Problem(CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest, result.Error, validationResult.Errors)),
            _ => Results.Problem(CreateProblemDetails("Bab Request", StatusCodes.Status500InternalServerError, result.Error))
        };

        private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
        => new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

        protected static IResult ResponseOk<T>(Result<T> result) => Results.Ok(result.Value);

        protected static IResult Redirect(string url) => Results.Redirect(url);
        protected static IResult ResponseOk(Result result) => Results.Ok();

        protected static IResult Challenge(AuthenticationProperties? properties = null,
        IList<string>? authenticationSchemes = null) => Results.Challenge(properties, authenticationSchemes);
    }
}
