using Link.Slicer.Application.Common.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Link.Slicer.Configurations
{
    public class NpgsqlHealthCheck : IHealthCheck
    {
        private readonly IApplicationDbContext _context;
        public NpgsqlHealthCheck(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var key = _context.Urls.Any(i => !i.DeletedAt.HasValue);
                return Task.FromResult(HealthCheckResult.Healthy("Healthy"));
            }
            catch (Exception)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy"));
            }
        }
    }
}
