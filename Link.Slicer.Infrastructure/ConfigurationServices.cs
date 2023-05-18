using Link.Slicer.Application.Common.Interfaces;
using Link.Slicer.Infrastructure.Logging;
using Link.Slicer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Link.Slicer.Infrastructure
{
    public static class ConfigurationServices
    {
        public static void AddInfrastructureServices(this WebApplicationBuilder builder)
        {
            // Add dbContext
            var connectionString = builder.Configuration?.GetSection("AppSettings:ConnectionStrings")["DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register services
            builder.Services.AddSingleton(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}
