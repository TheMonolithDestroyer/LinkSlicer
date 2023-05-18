using Serilog;
using Serilog.Settings.Configuration;

namespace Link.Slicer.Configurations
{
    public static class LoggerConfig
    {
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            var options = new ConfigurationReaderOptions { SectionName = "Serilog" };

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration, options)
                .CreateLogger();

            builder.Host.UseSerilog(logger);
        }
    }
}
