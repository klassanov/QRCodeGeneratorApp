using QRGeneratorApp.Core.Common.Messaging;

namespace QRGeneratorApp.Core.Orders.SSE
{
    public record class OrderPlacement(Guid Id, string CustomerName, string Text, DateTime PlacedAt): IIntegrationEvent;
    
}
