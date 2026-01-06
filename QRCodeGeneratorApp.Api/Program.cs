using Carter;
using Microsoft.OpenApi;
using QRCodeGeneratorApp.Api.BackgroundTasks;
using QRCodeGeneratorApp.Api.CustomMiddleware;
using QRCodeGeneratorApp.Api.ExceptionHandling;
using QRCodeGeneratorApp.Api.Healthcheck;
using QRCodeGeneratorApp.Infrastructure;
using QRCodeGeneratorApp.Persistence;
using QRCodeGeneratorApp.Persistence.DataSeeding;
using QRCodeGeneratorApp.ServiceDefaults;
using QRGeneratorApp.Core;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//TODO: Duplicated health checks registration, remove one of them after investigation
builder.AddServiceDefaults();

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

//Custom healthecks registration
builder.Services.RegisterHealthChecks(builder.Configuration);

//Long-running startup task
builder.Services.AddHostedService<StartupBackgroundService>();

//Background service to simulate order placements
builder.Services.AddHostedService<OrderPlacementBackgroundService>();

//Register services from the Core project
builder.Services.RegisterCoreServices();

builder.Services.AddCustomMiddleware();

builder.Services.RegisterPersistenceServices();

builder.Services.RegisterInfrastructureServices();

builder.RegisterMongoDbClient();

var app = builder.Build();

//Data seeding if necessary
app.Services.SeedOrders();

app.MapDefaultEndpoints();

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

//Use my custom middleware
app.UseCustomMiddleware();

//Export to a separate file for better readability
app.MapHealthChecksWithEndpoints();

app.Run();


