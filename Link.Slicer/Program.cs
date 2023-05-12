using Autofac.Extensions.DependencyInjection;
using Link.Slicer.Database;
using Serilog;
using Link.Slicer.Utils;
using Link.Slicer.Models;
using Link.Slicer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Serilog
var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration, sectionName: "Serilog")
                .CreateLogger();
builder.Host.UseSerilog(logger);

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.RegisterCustomServices();

// AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
// DbContext
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddEndpointsApiExplorer();
// Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddSwagger();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { }
// SwaggerUI
app.UseSwaggerDocument(builder.Environment, builder.Configuration);
app.UseRouting();
app.UseAuthorization();
// Bot detection
app.UseMiddleware<BotDetectionMiddleware>();
// RoutingEnpoints
app.UseRoutingEnpoints();
app.Run();
