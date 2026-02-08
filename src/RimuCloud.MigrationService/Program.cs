using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using RimuCloud.Infrastructure.DependencyInjection.Configurations;
using RimuCloud.Infrastructure.Postgres;
using RimuCloud.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<ApiDbInitializer>();

builder.Services.Configure<DbConfig>(options =>
{
    builder.Configuration.GetSection("DbConfig").Bind(options);
});


builder.Services.AddDbContextPool<AppDbContext>((sp, opt) =>
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
    opt.UseNpgsql(con.ConnectionString, options =>
    {
        options.MigrationsAssembly("RimuCloud.MigrationService");
    });
});


var host = builder.Build();
host.Run();
