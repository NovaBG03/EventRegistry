using EventRegistry.Messaging.Messages;

namespace EventRegistry.Messaging.Publishing.Contracts;

public interface INotificationsTopicPublisher
{
    public Task PublishNotificationAsync(NotificationDataMessage message, CancellationToken ct = default);
}
