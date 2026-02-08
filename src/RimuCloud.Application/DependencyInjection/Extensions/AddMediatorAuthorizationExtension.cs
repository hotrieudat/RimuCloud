using Mediator;
using Microsoft.Extensions.DependencyInjection;
using RimuCloud.Application.Behaviors;
using RimuCloud.Application.Interfaces;
using System.Reflection;

namespace RimuCloud.Application.DependencyInjection.Extensions
{
    public static class AddMediatorAuthorizationExtension
    {
        public static IServiceCollection AddMediatorAuthorization(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));
            AddAuthorizationHandlers(services, assembly);

            return services;
        }

        private static IServiceCollection AddAuthorizationHandlers(IServiceCollection services, Assembly assembly)
        {
            var authHandlerOpenType = typeof(IAuthorizationHandler<>);
            GetTypesAssignableTo(assembly, authHandlerOpenType)
                .ForEach((concretion) =>
                {
                    foreach (var implementedInterface in concretion.ImplementedInterfaces)
                    {
                        if (!implementedInterface.IsGenericType)
                            continue;
                        if (implementedInterface.GetGenericTypeDefinition() != authHandlerOpenType)
                            continue;

                        services.AddTransient(implementedInterface, concretion);
                    }
                });

            return services;
        }

#pragma warning disable CS8603 // Possible null reference return.
        private static List<TypeInfo> GetTypesAssignableTo(Assembly assembly, Type compareType)
        {
            return assembly.DefinedTypes.Where(x => x.IsClass
                                                    && !x.IsAbstract
                                                    && x != compareType
                                                    && x.GetInterfaces()
                                                        .Any(i => i.IsGenericType
                                                                  && i.GetGenericTypeDefinition() == compareType))?.ToList();
        }
#pragma warning restore CS8603
    }
}
