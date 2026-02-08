using FluentValidation;
using Mapster;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using RimuCloud.Application.Behaviors;
using System.Reflection;

namespace RimuCloud.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            services
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ErrorLoggingBehaviour<,>))
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>));
            //.AddSingleton(typeof(IPipelineBehavior<,>), typeof(TestBehaviour<,>));

            //Add Validators
            services.AddValidatorsFromAssemblyContaining(typeof(AssemblyReference), lifetime: ServiceLifetime.Singleton);

            // Register all `IAuthorizer` implementations for a given assembly
            services.AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());
            // Adds the transient pipeline behavior and additionally registers all `IAuthorizationHandlers` for a given assembly
            services.AddMediatorAuthorization(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}