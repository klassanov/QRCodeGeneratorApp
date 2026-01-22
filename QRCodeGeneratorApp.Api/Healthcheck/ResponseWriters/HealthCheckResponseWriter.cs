using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QRCodeGeneratorApp.Api.Healthcheck.ResponseWriters
{
    public class HealthCheckResponseWriter
    {
        public static async Task WriteJsonResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    exception = entry.Value.Exception?.Message,
                    duration = entry.Value.Duration.ToString()
                })
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
