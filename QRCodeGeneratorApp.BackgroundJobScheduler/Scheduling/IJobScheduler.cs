
namespace QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling
{
    internal interface IJobScheduler
    {
        Task<bool> ScheduleFireAndForgetJob(string functionName, DateTime executionTime);
        
        Task<bool> ScheduleRecurringJob(string functionName, string cronExpression);
    }
}