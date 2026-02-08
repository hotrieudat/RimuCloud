using Microsoft.Extensions.DependencyInjection;
using RimuCloud.Application.Interfaces;
using System.Reflection;

namespace RimuCloud.Application.DependencyInjection.Extensions
{
    public static class AddAuthorizersFromAssemblyExtension
    {
        public static IServiceCollection AddAuthorizersFromAssembly(
                this IServiceCollection services,
                Assembly assembly,
                ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var authorizerType = typeof(IAuthorizer<>);
            GetTypesAssignableTo(assembly, authorizerType).ForEach((type) =>
            {
                foreach (var implementedInterface in type.ImplementedInterfaces)
                {
                    if (!implementedInterface.IsGenericType)
                        continue;
                    if (implementedInterface.GetGenericTypeDefinition() != authorizerType)
                        continue;

                    var serviceType = implementedInterface.ContainsGenericParameters ? authorizerType : implementedInterface;

                    services.Add(new ServiceDescriptor(serviceType, type, lifetime));
                }
            });
            return services;
        }

#pragma warning disable CS8603 // Possible null reference return.
        private static List<TypeInfo> GetTypesAssignableTo(Assembly assembly, Type compareType)
        {
            var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                                && !x.IsAbstract
                                && x != compareType
                                && x.GetInterfaces()
                                        .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == compareType))?.ToList();

            return typeInfoList;
        }
#pragma warning restore CS8603 // Possible null reference return.
    }
}
