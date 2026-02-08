
var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.RimuCloud_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.RimuCloud_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.RimuCloud_MigrationService>("rimucloud-migrationservice");

builder.Build().Run();
