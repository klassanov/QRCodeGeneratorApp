using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QRCodeGeneratorApp.BackgroundJobScheduler.Jobs;
using QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling;
using TickerQ.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Service registrations
builder.Services.AddTickerQ();
builder.Services.AddScoped<IJobScheduler, JobScheduler>();

var app = builder.Build();


// Middleware registrations
app.UseTickerQ();


//Job Registration
using (var scope = app.Services.CreateScope())
{
    var jobScheduler = scope.ServiceProvider.GetRequiredService<IJobScheduler>();
    _ = await jobScheduler.ScheduleFireAndForgetJob(nameof(HelloWorldJob), DateTime.UtcNow.AddSeconds(10));

   _= await jobScheduler.ScheduleRecurringJob(nameof(PublishOrderPlacementJob), "*/1 * * * * *"); // Every 1 minute
}


await app.RunAsync();





