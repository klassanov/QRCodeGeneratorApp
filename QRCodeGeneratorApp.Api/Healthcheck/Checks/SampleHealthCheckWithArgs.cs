using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QRCodeGeneratorApp.Api.Healthcheck.Checks
{
    public class SampleHealthcheckWithArgs : IHealthCheck
    {
        private readonly string _arg1;
        private readonly int _arg2;
        public SampleHealthcheckWithArgs(string arg1, int arg2)
        {
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result"));
        }
    }
}
