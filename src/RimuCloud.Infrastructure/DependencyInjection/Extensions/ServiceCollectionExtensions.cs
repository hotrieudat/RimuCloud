using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace RimuCloud.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggerManager();
            return services;
        }
    }
}
