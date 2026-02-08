namespace RimuCloud.ApiService.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection CorsConfiguration(this IServiceCollection services)
        {
            //--------------------------
            // Allow all cors, please re-config if used
            //--------------------------

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            return services;
        }
    }
}
