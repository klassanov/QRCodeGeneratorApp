using System.Threading.Channels;
using QRGeneratorApp.Core.Common.Messaging;

namespace QRCodeGeneratorApp.Infrastructure.Messaging.InMemory
{
    //Comment for multiple clients: messages will be different for all the clients
    //A single Channel<T> cannot broadcast messages to multiple consumers.
    //Every time a message is read from the channel, the message is consumed, and no other consumer is going to get it.
    //If you want to broadcast all messages to all consumers, you'll have to create one dedicated Channel<T> per consumer.

    public class InMemoryEventBus<TEvent> : IEventBus<TEvent> where TEvent : class, IIntegrationEvent
    {
        private readonly Channel<TEvent> orderPlacementChannel;
        private readonly int maxCapacity = 100;

        public InMemoryEventBus()
        {
            this.orderPlacementChannel = Channel.CreateBounded<TEvent>(
                new BoundedChannelOptions(capacity: maxCapacity)
                {
                    FullMode = BoundedChannelFullMode.Wait
                });
        }

        public async Task PublishAsync(TEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            await this.orderPlacementChannel.Writer.WriteAsync(integrationEvent, cancellationToken);
        }

        public IAsyncEnumerable<TEvent> ReadAllAsync(CancellationToken cancellationToken = default)
        {
            return this.orderPlacementChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
