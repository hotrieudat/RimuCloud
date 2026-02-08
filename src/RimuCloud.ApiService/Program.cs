using Carter;
using Microsoft.AspNetCore.HttpOverrides;
using RimuCloud.ApiService.DependencyInjection.Extensions;
using RimuCloud.Infrastructure.DependencyInjection.Extensions;
using RimuCloud.Persistence.DependencyInjection.Extensions;
using RimuCloud.Application.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
MediatorDependencyInjectionExtensions.AddMediator(builder.Services, options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.CorsConfiguration();

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();

app.UseCors("AllowAll");

app.MapCarter();

app.Run();
