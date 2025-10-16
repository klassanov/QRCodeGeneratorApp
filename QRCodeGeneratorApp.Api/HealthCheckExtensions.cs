using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using QRCodeGeneratorApp.Api.Healthcheck.Checks;
using QRCodeGeneratorApp.Api.Healthcheck.ResponseWriters;
using RabbitMQ.Client;

namespace QRCodeGeneratorApp.Api;

public static class HealthCheckExtensions
{

    //Readiness vs Liveness Probes in Kubernetes  
    //| Probe Type    | Question         | What Happens on Failure(Kubernetes)   | Purpose                         |
    //| ------------- | ---------------- | ------------------------------------- | ------------------------------- |
    //| **Liveness**  | “Are you alive?” | Pod is **restarted**                  | Recover from crashes            |
    //| **Readiness** | “Are you ready?” | Pod is **removed from load balancer** | Avoid sending traffic too early |



    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        //Basic health proble
        var healthcheckBuilder = services.AddHealthChecks();

        //Add Custom Healthcheck Service (also possible to use healthcheckBuilder variable, cause AddHealthChecks() is idempotent and returns this variable)
        services.AddHealthChecks()
                        .AddCheck<SampleHealthCheck>(
            name: "Sample",
            failureStatus: HealthStatus.Degraded, //optional, by default it's Unhealthy, but we can set what to be in case of failure
            tags: ["sample"]); //optional, used for filtering

        //Another way to add the same custom health check
        //healthcheckBuilder.AddCheck<CustomHealthcheck>(
        //    name: "Sample",
        //    failureStatus: HealthStatus.Degraded,
        //    tags: ["sample"]);


        //Lambda function that returns always healthy result
        healthcheckBuilder.AddCheck(
            name: "self",
            check: () =>
            {
                return HealthCheckResult.Degraded();
            });


        //Pass arguments to healthcheck constructor
        healthcheckBuilder.AddTypeActivatedCheck<SampleHealthcheckWithArgs>(
            name: "SampleWithArgs",
            failureStatus: HealthStatus.Degraded,
            tags: ["samplewithargs"],
            args: ["argument1", 123]);


        //Use tags for filtering
        healthcheckBuilder.AddCheck<SampleHealthCheckWithTags>(
            name: "SampleWithTags",
            tags: ["tag1", "tag2"]);


        //Nice way to reuse tags defined in the healthcheck class
        healthcheckBuilder.AddCheck<SampleHealthCheckWIthTags2>(
            name: "SampleWithTags2",
            tags: SampleHealthCheckWIthTags2.Tags
        );

        //Add SQL Server healthcheck
        //AspNetCore.Diagnostics.HealthChecks library that we installed and is used here isn't maintained or supported by Microsoft.
        //It tests the conn string by creating an SELECT 1 query, so it should be lightweight.
        //We can also provdide a custom implementation for this, but should use a lightweight query to avoid overhead on the DB server.
        healthcheckBuilder.AddSqlServer(
            connectionString: configuration.GetConnectionString("AppUsersLocalDb")!,
            name: "SQL Server",
            tags: ["db", "sql", "sqlserver"]
        );

        //Add PostgreSQL healthcheck from the same library
        //Shall start docker to get it healthy
        healthcheckBuilder.AddNpgSql(
            connectionString: configuration.GetConnectionString("PostgresDocker")!,
            name: "PostgreSQL",
            tags: ["db", "sql", "postgresql"]
        );

        //Note: db probes as well as any other healthcheck can have a custom implementation, whatever U like

        services.AddSingleton<StartupHealthCheck>();


        //Readiness healthcheck
        healthcheckBuilder.AddCheck<StartupHealthCheck>(
            name: "Startup",
            failureStatus: HealthStatus.Unhealthy,
            tags: ["ready"]
        );


        //Publher: we have different config options here
        //healthcheckBuilder.Services.Configure<HealthCheckPublisherOptions>(options =>
        //{
        //    options.Delay = TimeSpan.FromSeconds(5); //Initial delay before starting publishing
        //    options.Period = TimeSpan.FromSeconds(10); //Period between executions
        //    options.Timeout = TimeSpan.FromSeconds(10); //Timeout for the whole publishing process
        //    options.Predicate = (check) => true; //Publish all healthchecks, can be filtered by tags as well
        //});
        //services.AddSingleton<IHealthCheckPublisher, SampleHealthCheckPublisher>();



        //services
        //.AddSingleton<IConnection>(sp =>
        //{
        //    var factory = new ConnectionFactory
        //    {
        //        Uri = new Uri("amqps://user:pass@host/vhost"),
        //    };
        //    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
        //});


        //healthcheckBuilder.AddRabbitMQ(
        //    name:"RabbitMQ",
        //    tags: ["rabbit"]);



        //Add UI for healthchecks
        services.AddHealthChecksUI(setupSettings: setup =>
                {
                    setup.SetEvaluationTimeInSeconds(Convert.ToInt32(TimeSpan.FromMinutes(10).TotalSeconds)); // Configures the UI to poll for healthchecks updates every 5 seconds
                    setup.AddHealthCheckEndpoint("Check All Health Check", "/healthz/all"); // Map health check endpoint)
                })
                .AddInMemoryStorage();

        return services;


    }


    public static void MapHealthChecksWithEndpoints(this WebApplication app)
    {
        //Routing: Map basic health endpoint (relative path or ?endpoint url?)
        //No filtering, all healthcheks will be executed, independently if they have tags or not
        app.MapHealthChecks("/healthz");

        


        //Routing: Map healthcheck with tags filtering
        app.MapHealthChecks("/healthz/tag1", new HealthCheckOptions()
        {
            //Only checks with tag1 will be included
            //Execution condition (tags are added during registration of healthchecks)
            Predicate = (check) => check.Tags.Contains("tag1")
        });


        //Customize Response status
        app.MapHealthChecks("/healthz/tag2", new HealthCheckOptions()
        {
            //Filtering
            Predicate = (check) => check.Tags.Contains("tag2"),

            //Custom response status
            ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },


            //Custom response writer: can be lambda or a separate method
            //There is another good example on the Microsoft docs
            ResponseWriter = async (HttpContext context, HealthReport report) =>
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
        });


        //Databse probes
        app.MapHealthChecks("/healthz/db", new HealthCheckOptions()
        {
            //Filtering
            Predicate = (check) => check.Tags.Contains("db"),

            //I am using a static class and method here for reusability
            ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
        });

        //Readiness probe endpoint
        app.MapHealthChecks("/healthz/ready", new HealthCheckOptions()
        {
            Predicate = (check) => check.Tags.Contains("ready"),
        });


        //Liveness probe endpoint => does not run any of the registered healthchecks, just returns Healthy if the app is running
        //Excludes all checks and reports a Healthy status for all calls.
        //Note the difference with the first endpoint defined that runs all the healthcheks as a contrast
        app.MapHealthChecks("/healthz/live", new HealthCheckOptions()
        {
            //No checks, just return 200 OK
            Predicate = (_) => false
        });

        //Note: UseHealthChecks vs. MapHealthChecks
        //https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0#usehealthchecks-vs-maphealthchecks


        app.MapHealthChecks("/healthz/rabbit", new HealthCheckOptions()
        {
            Predicate = (check) => check.Tags.Contains("rabbit"),
            ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
        });

        //UI implementation

        //Call all healthchecks with UI response writer
        app.MapHealthChecks("/healthz/all", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseRouting()
           .UseEndpoints(config => config.MapHealthChecksUI());


    }
}