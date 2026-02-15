using TickerQ.Utilities.Base;

namespace QRCodeGeneratorApp.BackgroundJobScheduler.Jobs
{
    internal class HelloWorldJob
    {
        [TickerFunction(nameof(HelloWorldJob))]
        public async Task HelloWorld(TickerFunctionContext context, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Scheduled at: {DateTime.UtcNow:HH:mm:ss}");
        }
    }
}
