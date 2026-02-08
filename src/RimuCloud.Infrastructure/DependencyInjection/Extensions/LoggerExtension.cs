using Microsoft.Extensions.DependencyInjection;
using RimuCloud.Abstractions.Logger;
using RimuCloud.Logger.LoggerManager;

namespace RimuCloud.Infrastructure.DependencyInjection.Extensions
{
    public static class LoggerCollectionExtensions
    {
        public static IServiceCollection AddLoggerManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();

            return services;
        }
    }
}
