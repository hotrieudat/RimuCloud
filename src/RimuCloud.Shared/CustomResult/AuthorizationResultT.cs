namespace RimuCloud.Shared.CustomResult
{
    public class AuthorizationResult<TValue> : Result<TValue>, IAuthorizationResult
    {
        public bool IsAuthorized { get; }

        private AuthorizationResult(bool isAuthorized, Error error)
            : base(default, isAuthorized, error)
        {
            IsAuthorized = isAuthorized;
        }

        public static AuthorizationResult<TValue> Succeed()
        {
            return new AuthorizationResult<TValue>(true, Error.None);
        }

        public static AuthorizationResult<TValue> Fail(Error? error = null)
        {
            if (error == null)
                return new AuthorizationResult<TValue>(false, IAuthorizationResult.AuthorizationError);

            return new AuthorizationResult<TValue>(false, error);
        }
    }
}
