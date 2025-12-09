
namespace QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling
{
    internal interface IJobScheduler
    {
        Task<bool> ScheduleFireAndForgetJob(string functionName, DateTime executionTime);
    }
}