using Link.Slicer.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Link.Slicer.Utils
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Link.Slicer v1",
                    Version = "v1",
                    Description = "A v1 WebApi for managing Link.Slicer services"
                });
            });
        }

        public static void UseSwaggerDocument(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((openApiDocument, httpRequest) =>
                {
                    openApiDocument.Servers = new List<OpenApiServer>();
                    var apiServer = new OpenApiServer
                    {
                        Url = configuration.GetSection(nameof(AppSettings) + ":DefaultDomain").Value
                    };
                    openApiDocument.Servers.Add(apiServer);
                });
            });

            app.UseSwaggerUI(options => { options.DocExpansion(DocExpansion.None); });
        }
    }
}
