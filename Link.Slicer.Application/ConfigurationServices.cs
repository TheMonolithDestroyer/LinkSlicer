using Link.Slicer.Application.Services.UrlService;
using Microsoft.Extensions.DependencyInjection;

namespace Link.Slicer.Application
{
    public static class ConfigurationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUrlService, UrlService>();
        }
    }
}
