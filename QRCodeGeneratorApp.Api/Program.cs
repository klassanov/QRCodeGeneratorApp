using System.Net;
using Carter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using QRCodeGeneratorApp.Api.ExceptionHandling;
using QRCodeGeneratorApp.Api.Healthchecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    //Customize the generated Open Api document 
    options.AddDocumentTransformer((document, context, canellationToken) =>
    {
        document.Info.Title = "Customized Open Api Title - Generate QR code";
        document.Info.Contact = new OpenApiContact
        {
            Name = "Alexander Klassanov",
            Email = "test@test.com",
            Url = new Uri("https://test.com")
        };
        document.Info.License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        };
        return Task.CompletedTask;
    });
});



//Global exception handling and custom exception handlers=> the order matters
//ASP.NET Core will use the first one that returns true from TryHandleAsync.
builder.Services.AddExceptionHandler<QRCodeExceptionHandler>()
                .AddExceptionHandler<GlobalExceptionHandler>();



//Add ProblemDetails support
builder.Services.AddProblemDetails();

//Automatic minimal APIs registration service
builder.Services.AddCarter();


//Basic health proble
var healthcheckBuilder = builder.Services.AddHealthChecks();

//Add Custom Healthcheck Service (also possible to use healthcheckBuilder variable, cause AddHealthChecks() is idempotent and returns this variable)
builder.Services.AddHealthChecks()
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
    connectionString: builder.Configuration.GetConnectionString("AppUsersLocalDb")!,
    name: "SQL Server",
    tags: ["db", "sql", "sqlserver"]
);

var app = builder.Build();

//Use custom global exception handling
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //Substitute Swagger UI, can be customized as well
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


//Automatic minimal APIs registration
app.MapCarter();


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

app.MapHealthChecks("/healthz/db", new HealthCheckOptions()
{
    //Filtering
    Predicate = (check) => check.Tags.Contains("db")
});





app.Run();


