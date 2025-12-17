using EventRegistry.Data.Entities;
using EventRegistry.Data.Repositories.Contracts;
using EventRegistry.EventProcessor.Services.Contracts;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Publishing.Contracts;

namespace EventRegistry.EventProcessor.Services;

public class EventProcessorService(
    IEventRepository eventRepository,
    IApplicationRepository applicationRepository,
    INotificationsTopicPublisher notificationsTopicPublisher
) : IEventProcessorService
{
    public async Task ProcessEventMessageAsync(EventMessage eventMessage)
    {
        var entity = new Event
        {
            Id = eventMessage.EventId,
            ApplicationId = eventMessage.ApplicationId,
            Category = Enum.Parse<EventCategory>(eventMessage.Category, true),
            Message = eventMessage.Message,
            Timestamp = eventMessage.Timestamp,
            Metadata = eventMessage.Metadata
        };

        entity = await eventRepository.AddAsync(entity);

        if (entity.Category is EventCategory.Critical or EventCategory.Error)
        {
            var application = await applicationRepository.GetByIdAsync(entity.ApplicationId);

            var notificationRequest = new NotificationDataMessage
            {
                EventId = entity.Id,
                ApplicationId = entity.ApplicationId,
                Category = entity.Category.ToString(),
                Message = entity.Message,
                Timestamp = entity.Timestamp,
                Metadata = entity.Metadata
            };

            await notificationsTopicPublisher.PublishNotificationAsync(notificationRequest);
        }
    }
}
