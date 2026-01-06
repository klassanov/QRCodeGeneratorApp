namespace QRGeneratorApp.Core.Common.Messaging
{
    public interface IEventBus<TEvent> where TEvent : class, IIntegrationEvent
    {
        Task PublishAsync(TEvent integrationEvent, CancellationToken cancellationToken = default);
        
        IAsyncEnumerable<TEvent> ReadAllAsync(CancellationToken cancellationToken = default);
    }
}
