using Microsoft.Extensions.Configuration;

namespace QRCodeGeneratorApp.AppHost
{
    internal static class Extensions
    {
        extension(IResourceBuilder<ProjectResource> builder)
        {
            public IResourceBuilder<ProjectResource> WithEnvironmentVariables(IConfiguration configuration, params string[] configKeys)
            {
                if (configKeys is null || configKeys.Length == 0)
                {
                    return builder;
                }

                foreach (var key in configKeys)
                {
                    var value = configuration[key];
                    if (!string.IsNullOrEmpty(value))
                    {
                        builder = builder.WithEnvironment(key, value);
                    }
                }

                return builder;
            }


            public IResourceBuilder<ProjectResource> WithOtelEnvironmentVariables(IConfiguration configuration)
            {
                return builder.WithEnvironmentVariables(configuration,
                              "OTEL_RESOURCE_ATTRIBUTES",
                              "OTEL_EXPORTER_OTLP_PROTOCOL",
                              "OTEL_EXPORTER_OTLP_HEADERS",
                              "OTEL_EXPORTER_OTLP_ENDPOINT");
            }
        }
    }
}
