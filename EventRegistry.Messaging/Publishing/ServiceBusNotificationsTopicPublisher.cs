using System.Text.Json;
using Azure.Messaging.ServiceBus;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Publishing.Contracts;
using Microsoft.Extensions.Logging;

namespace EventRegistry.Messaging.Publishing;

public class ServiceBusNotificationsTopicPublisher(
    ILogger<ServiceBusNotificationsTopicPublisher> logger,
    ServiceBusClient client
) : ServiceBusPublisher(DestinationName, client), INotificationsTopicPublisher
{
    private const string DestinationName = "notifications-topic";

    public async Task PublishNotificationAsync(NotificationDataMessage message, CancellationToken ct = default)
    {
        var sbMessage = new ServiceBusMessage(JsonSerializer.Serialize(message))
        {
            ContentType = "application/json",
        };

        await Sender.SendMessageAsync(sbMessage, ct);
        logger.LogInformation("Published message to {Destination}", DestinationName);
    }
}
