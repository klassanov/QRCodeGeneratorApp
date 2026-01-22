using System;
using System.Collections.Generic;
using System.Text;
using TickerQ.Utilities.Entities;
using TickerQ.Utilities.Interfaces.Managers;

namespace QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling
{
    internal class JobScheduler: IJobScheduler
    {
        private readonly ITimeTickerManager<TimeTickerEntity> timeTickerManager;
        
        public JobScheduler(ITimeTickerManager<TimeTickerEntity> timeTickerManager)
        {
            this.timeTickerManager = timeTickerManager;
        }

        public async Task<bool> ScheduleFireAndForgetJob(string functionName, DateTime executionTime)
        {
            var result = await timeTickerManager.AddAsync(new TimeTickerEntity()
            {
                Function = functionName,
                ExecutionTime = executionTime
            });

            if (result.IsSucceeded)
            {
                Console.WriteLine($"Job scheduled successfully! Job ID: {result.Result.Id}");
            }

            return result.IsSucceeded;
        }
    }
}
