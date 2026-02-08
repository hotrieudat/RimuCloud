namespace RimuCloud.Shared.CustomResult
{
    public interface IAuthorizationResult
    {
        bool IsAuthorized { get; }
        Error Error { get; }

        public static readonly Error AuthorizationError = new(
            "AuthorizationError",
            "A Authorization proble");
    }
}
