using System;
using System.Collections.Generic;
using System.Text;
using TickerQ.Utilities.Base;

namespace QRCodeGeneratorApp.BackgroundJobScheduler.Jobs
{
    public class HelloWorldJob
    {
        [TickerFunction("HelloWorldFunction")]
        public async Task HelloWorld(TickerFunctionContext context, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Scheduled at: {DateTime.UtcNow:HH:mm:ss}");
        }
    }
}
