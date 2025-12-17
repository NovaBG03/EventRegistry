using Azure.Messaging.ServiceBus;
using EventRegistry.Messaging.Messages;

namespace EventRegistry.Messaging.Subscribing;

public abstract class NotificationsTopicBackgroundProcessor(ServiceBusClient client, string subscriptionName)
    : JsonServiceBusBackgroundProcessor<NotificationDataMessage>(client, TopicName, subscriptionName)
{
    private const string TopicName = "notifications-topic";
}
