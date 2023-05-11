using Microsoft.EntityFrameworkCore;

namespace Link.Slicer.Database
{
    public static class AppDbBuilder
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration?.GetSection("AppSettings:ConnectionStrings")["DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly("Link.Slicer")
                        .EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null)));
        }
    }
}
