using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling;
using TickerQ.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Service registrations
builder.Services.AddTickerQ();
builder.Services.AddScoped<IJobScheduler, JobScheduler>();

var app = builder.Build();


// Middleware registrations
app.UseTickerQ();

using (var scope = app.Services.CreateScope())
{
    var jobScheduler = scope.ServiceProvider.GetRequiredService<IJobScheduler>();
    var result = await jobScheduler.ScheduleFireAndForgetJob("HelloWorldFunction", DateTime.UtcNow.AddSeconds(10));
}


await app.RunAsync();





