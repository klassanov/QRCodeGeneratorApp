using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QRCodeGeneratorApp.Api.Healthcheck.Checks
{
    public class StartupHealthCheck : IHealthCheck
    {
        private bool isCompleted;

        public void SignaStartupCompleted()
        {
            isCompleted = true;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return isCompleted
                ? Task.FromResult(HealthCheckResult.Healthy("The application has completed startup."))
                : Task.FromResult(HealthCheckResult.Unhealthy("The application has not yet completed startup."));
        }
    }
}
