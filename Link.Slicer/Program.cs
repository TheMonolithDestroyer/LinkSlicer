using Link.Slicer.Infrastructure;
using Link.Slicer.Configurations.Middlewares;
using Link.Slicer.Configurations;
using Link.Slicer.Application;
using Link.Slicer.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.AddLogger();
builder.Services.AddApplicationServices();
builder.AddInfrastructureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { }

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
