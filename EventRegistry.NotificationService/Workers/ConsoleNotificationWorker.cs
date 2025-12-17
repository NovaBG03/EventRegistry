using Azure.Messaging.ServiceBus;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Subscribing;

namespace EventRegistry.NotificationService.Workers;

public class ConsoleNotificationWorker(
    ILogger<ConsoleNotificationWorker> logger,
    ServiceBusClient client)
    : NotificationsTopicBackgroundProcessor(client, SubscriptionName)
{
    private const string SubscriptionName = "console-subscription";

    protected override Task ProcessObjectMessage(NotificationDataMessage message)
    {
        var logLevel = Enum.TryParse<LogLevel>(message.Category, ignoreCase: true, out var level)
            ? level
            : LogLevel.Information;

        logger.Log(
            logLevel,
            "Notification: [{Timestamp}] (AppID: {ApplicationId}): {Message}, Metadata: {Metadata}",
            message.Timestamp,
            message.ApplicationId,
            message.Message,
            message.Metadata);

        return Task.CompletedTask;
    }

    protected override Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        logger.LogWarning(
            args.Exception,
            "Service Bus error in {Source}. Entity: {EntityPath}, Namespace: {Namespace}, Reason: {ErrorSource}",
            nameof(ConsoleNotificationWorker),
            args.EntityPath,
            args.FullyQualifiedNamespace,
            args.ErrorSource
        );
        return Task.CompletedTask;
    }
}
