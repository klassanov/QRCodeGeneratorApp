using Microsoft.OpenApi.Models;
using QRCodeGeneratorApp.Api.ExceptionHandling;
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");



app.MapGet("/exception/{exceptionType}", (string exceptionType) =>
{
    var ex = exceptionType.ToLower() switch
    {
        "application" => new ApplicationException("This is a test application exception from /exception endpoint"),
        "invalidoperation" => new InvalidOperationException("This is a test invalid operation exception from /exception endpoint"),
        _ => new Exception("This is a test exception from /exception endpoint")
    };

    throw ex;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
