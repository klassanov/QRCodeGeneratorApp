using System.Threading.Channels;
using Carter;
using QRGeneratorApp.Core.Common.Messaging;
using QRGeneratorApp.Core.Orders.SSE;

namespace QRCodeGeneratorApp.Api.Endpoints.Orders
{
    public class GetOrdersSSE : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/realtime", (
                IEventBus<OrderPlacement> eventBus,
                CancellationToken cancellationToken) =>
            {
                // 1. ReadAllAsync returns an IAsyncEnumerable
                // 2. Results.ServerSentEvents tells the browser: "Keep this connection open"
                // 3. New data is pushed to the client as soon as it enters the channel

                //Comment for multiple clients: messages will be different for all the clients
                //A single Channel<T> cannot broadcast messages to multiple consumers.
                //Every time a message is read from the channel, the message is consumed, and no other consumer is going to get it.
                //If you want to broadcast all messages to all consumers, you'll have to create one dedicated Channel<T> per consumer.

                return Results.ServerSentEvents(eventBus.ReadAllAsync(cancellationToken));
            });
        }
    }
}
