namespace RimuCloud.Shared.CustomResult
{
    public class AuthorizationResult : Result, IAuthorizationResult
    {
        public bool IsAuthorized { get; }

        private AuthorizationResult(bool isAuthorized, Error error)
            : base(isAuthorized, error)
        {
            IsAuthorized = isAuthorized;
        }

        public static AuthorizationResult Succeed()
        {
            return new AuthorizationResult(true, Error.None);
        }

        public static AuthorizationResult Fail(Error? error = null)
        {
            if (error == null)
                return new AuthorizationResult(false, IAuthorizationResult.AuthorizationError);

            return new AuthorizationResult(false, error);
        }
    }
}
