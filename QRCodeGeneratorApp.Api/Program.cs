using Carter;
using Microsoft.OpenApi;
using QRCodeGeneratorApp.Api.CustomMiddleware;
using QRCodeGeneratorApp.Api.ExceptionHandling;
using QRCodeGeneratorApp.Api.Healthcheck;
using QRCodeGeneratorApp.Api.StartupTasks;
using QRGeneratorApp.Core;
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

//Custom healthecks registration
builder.Services.RegisterHealthChecks(builder.Configuration);

//Long-running startup task
builder.Services.AddHostedService<StartupBackgroundService>();

//Register services from the Core project
builder.Services.RegisterCoreServices();

builder.Services.AddCustomMiddleware();


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

//Use my custom middleware
app.UseCustomMiddleware();

//Export to a separate file for better readability
app.MapHealthChecksWithEndpoints();

app.Run();


