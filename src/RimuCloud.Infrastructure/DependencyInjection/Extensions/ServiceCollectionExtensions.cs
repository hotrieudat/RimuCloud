using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using RimuCloud.Infrastructure.DependencyInjection.Configurations;
using RimuCloud.Infrastructure.Postgres;

namespace RimuCloud.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistencePostgresSQL(this IServiceCollection services, IConfiguration configuration)
        {
            // -------------------
            // Bind config option
            // -------------------
            services.Configure<DbConfig>(options =>
            {
                configuration.GetSection("DbConfig").Bind(options);
            });

            // -------------------
            // Build DI connection DB
            // -------------------
            services.AddDbContextPool<AppDbContext>((sp, opt) =>
            {
                var cfg = sp.GetRequiredService<IOptions<DbConfig>>().Value;
                var con = new NpgsqlConnectionStringBuilder()
                {
                    Host = cfg.Host,
                    Port = cfg.Port,
                    Username = cfg.User,
                    Password = cfg.Password,
                    Database = cfg.DbName,
                };

                // -------------------
                // Add Timeout for PgSQL
                // -------------------
                opt.UseNpgsql(con.ConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout((int)TimeSpan.FromSeconds(cfg.TimeoutSeconds).TotalMilliseconds);
                });
            });

            return services;
        }
    }
}
