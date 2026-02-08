namespace RimuCloud.Shared.CustomResult
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation proble");
        Error[] Errors { get; }
    }
}
