
namespace RimuCloud.Shared.CustomResult
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public bool Equals(Error? other)
        {
            if (other is null)
                return false;

            // So sánh dựa trên giá trị của Code và Message
            return Code == other.Code && Message == other.Message;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Error);
        }

        public override int GetHashCode()
        {
            // Tạo một hashcode dựa trên Code và Message
            return HashCode.Combine(Code, Message);
        }

        public override string ToString()
        {
            return $"Error: {Code} - {Message}";
        }

        // Toán tử == và !=
        public static bool operator ==(Error? left, Error? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Error? left, Error? right)
        {
            return !Equals(left, right);
        }
    }
}
