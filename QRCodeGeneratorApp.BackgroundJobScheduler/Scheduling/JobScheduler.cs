using System;
using System.Collections.Generic;
using System.Text;
using TickerQ.Utilities;
using TickerQ.Utilities.Entities;
using TickerQ.Utilities.Interfaces.Managers;

namespace QRCodeGeneratorApp.BackgroundJobScheduler.Scheduling
{
    internal class JobScheduler: IJobScheduler
    {
        private readonly ITimeTickerManager<TimeTickerEntity> timeTickerManager;
        private readonly ICronTickerManager<CronTickerEntity> cronTickerManager;

        public JobScheduler(
            ITimeTickerManager<TimeTickerEntity> timeTickerManager,
            ICronTickerManager<CronTickerEntity> cronTickerManager)
        {
            this.timeTickerManager = timeTickerManager;
            this.cronTickerManager = cronTickerManager;
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

        public async Task<bool> ScheduleRecurringJob(string functionName, string cronExpression)
        {
            var result = await cronTickerManager.AddAsync(new CronTickerEntity()
            {
               Function = functionName,
               Expression = cronExpression              
            });
           
            if (result.IsSucceeded)
            {
                Console.WriteLine($"Recurring job scheduled successfully! Job ID: {result.Result.Id}");
            }

            return result.IsSucceeded;
        }
    }
}
