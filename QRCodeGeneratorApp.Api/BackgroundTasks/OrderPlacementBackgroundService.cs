
using QRGeneratorApp.Core.Common.Messaging;
using QRGeneratorApp.Core.Orders.SSE;

namespace QRCodeGeneratorApp.Api.BackgroundTasks
{
    internal class OrderPlacementBackgroundService : BackgroundService
    {
        private readonly IEventBus<OrderPlacement> eventBus;

        public OrderPlacementBackgroundService(IEventBus<OrderPlacement> eventBus)
        {
            this.eventBus = eventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await eventBus.PublishAsync(
                    new OrderPlacement(Guid.NewGuid(), "Alex", Guid.NewGuid().ToString(), DateTime.UtcNow),
                    stoppingToken);


                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
