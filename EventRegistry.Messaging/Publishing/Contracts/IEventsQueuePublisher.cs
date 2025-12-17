using EventRegistry.Messaging.Messages;

namespace EventRegistry.Messaging.Publishing.Contracts;

public interface IEventsQueuePublisher
{
    public Task PublishEventAsync(EventMessage message, CancellationToken ct = default);
}
