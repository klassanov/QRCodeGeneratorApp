using Microsoft.Extensions.Diagnostics.HealthChecks;
using static System.Net.Mime.MediaTypeNames;

namespace QRCodeGeneratorApp.Api.Healthcheck.Publishers
{
    public class SampleHealthCheckPublisher : IHealthCheckPublisher
    {
        //Simple implementation that writes health check results to the console
        //Can be published everywhere
        
        //AspNetCore.Diagnostics.HealthChecks:

        //Includes publishers for several systems, including Application Insights.
        //Is not maintained or supported by Microsoft

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            report.Entries.ToList().ForEach(entry =>
            {
                Console.WriteLine($"Health check: {entry.Key}, Status: {entry.Value.Status}, Description: {entry.Value.Description}, Exception: {entry.Value.Exception}");
            });
            Console.WriteLine($"Overall status: {report.Status}");

            return Task.CompletedTask;
        }
    }
}
