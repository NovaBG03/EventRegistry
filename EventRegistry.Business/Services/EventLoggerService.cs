using EventRegistry.Business.Services.Contracts;
using EventRegistry.Data.Entities;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Publishing.Contracts;

namespace EventRegistry.Business.Services;

public class EventLoggerService(IEventsQueuePublisher eventsQueuePublisher) : IEventLoggerService
{
    public async Task<(Guid EventId, string Status)> LogEventAsync(
        Guid applicationId,
        string category,
        string message,
        DateTimeOffset timestamp,
        Dictionary<string, object>? metadata)
    {
        var eventMessage = new EventMessage
        {
            EventId = Guid.CreateVersion7(),
            ApplicationId = applicationId,
            Category = Enum.Parse<EventCategory>(category, ignoreCase: true).ToString(),
            Message = message,
            Timestamp = timestamp,
            Metadata = metadata
        };

        await eventsQueuePublisher.PublishEventAsync(eventMessage);

        return (eventMessage.EventId, "queued");
    }
}
