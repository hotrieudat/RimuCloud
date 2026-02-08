using Microsoft.Extensions.Configuration;
using RimuCloud.Abstractions.Logger;
using Serilog;

namespace RimuCloud.Logger.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {

        private readonly ILogger _logger;

        public LoggerManager(IConfiguration configuration)
        {
            _logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }

        public void Information(string message) => _logger.Information(message);
        public void Information<T>(string message, T obj) => _logger.Information(message, obj);
        public void Warning(string message) => _logger.Warning(message);
        public void Warning<T>(string message, T obj) => _logger.Warning(message, obj);
        public void Error(string message) => _logger.Error(message);
        public void Error(Exception ex, string message) => _logger.Error(ex, message);
        public void Error<T>(Exception ex, string message, T obj) => _logger.Error(ex, message, obj);
        public void Debug(string message) => _logger.Debug(message);
        public void Debug<T>(string message, T obj) => _logger.Debug(message, obj);
    }
}
