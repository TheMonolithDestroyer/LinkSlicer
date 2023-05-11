using Autofac.Extensions.DependencyInjection;
using Autofac;
using Link.Slicer.Database;
using Link.Slicer.Services;
using Serilog;
using Link.Slicer.Utils;
using Link.Slicer.Models;
using Link.Slicer.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration, sectionName: "Serilog")
                .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Host.ConfigureContainer<ContainerBuilder>(autofacContainerBuilder =>
{
    autofacContainerBuilder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();
    autofacContainerBuilder.RegisterType<UrlService>().As<IUrlService>().InstancePerLifetimeScope();
});
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { }

app.UseSwaggerDocument(builder.Environment, builder.Configuration);
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<BotDetectionMiddleware>();

app.UseEndpoints(endpoint =>
{
    //endpoint.MapGet("{*path}", async context =>
    //{
    //    string path = context.Request.Path.Value;
    //    await context.Response.WriteAsync($"Path: {path}");
    //});
    endpoint.MapControllerRoute
    (
        name: "Redirect", 
        pattern: $"/s7", 
        defaults: new 
        {
            controller = "UrlController", 
            action = "Redirect"
        });

    endpoint.MapControllers();
});
app.Run();
