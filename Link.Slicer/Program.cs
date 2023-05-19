using Link.Slicer.Infrastructure;
using Link.Slicer.Configurations.Middlewares;
using Link.Slicer.Configurations;
using Link.Slicer.Application;
using Link.Slicer.Application.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.AddLogger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();
builder.Services.AddApplicationServices();
builder.AddInfrastructureServices();
builder.Services.AddHealthChecks()
    .AddCheck<NpgsqlHealthCheck>("health-npgsql");

var app = builder.Build();
if (app.Environment.IsDevelopment()) { }

app.MapHealthChecks("health", new HealthCheckOptions{
    Predicate = healthCheck => healthCheck.Name.Equals("health")
});
app.MapHealthChecks("health-npgsql", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Name.Equals("health-npgsql")
});

// SwaggerUI
app.UseSwaggerDocument(builder.Environment, builder.Configuration);
app.UseRouting();
app.UseAuthorization();
// Global exception handler
app.UseMiddleware<ExceptionHandlerMiddleware>();
// Bot detection
app.UseMiddleware<BotDetectionMiddleware>();
// RoutingEnpoints
app.UseRoutingEnpoints();
app.Run();
