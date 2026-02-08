namespace RimuCloud.Abstractions.Logger
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Information<T>(string message, T obj);
        void Warning(string message);
        void Warning<T>(string message, T obj);
        void Error(string message);
        void Error(Exception ex, string message);
        void Error<T>(Exception ex, string message, T obj);
        void Debug(string message);
        void Debug<T>(string message, T obj);
    }
}
