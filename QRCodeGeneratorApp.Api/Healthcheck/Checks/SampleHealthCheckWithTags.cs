using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QRCodeGeneratorApp.Api.Healthcheck.Checks
{
    public class SampleHealthCheckWithTags : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            //Randomly return healthy or unhealthy result
            //NextSingle() returns a float between 0.0 and 1.0
            bool isHealthy = Random.Shared.NextSingle() > 0.5;

            return isHealthy
                ? Task.FromResult(HealthCheckResult.Healthy("A healthy result with tags"))
                : Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
