using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QRCodeGeneratorApp.Api.Healthchecks
{
    public class SampleHealthCheckWIthTags2 : IHealthCheck
    {
        public static readonly string[] Tags = [ "tag2", "tag3" ];

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
