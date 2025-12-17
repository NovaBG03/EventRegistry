using System.Text.Json;
using Azure.Messaging.ServiceBus;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Publishing.Contracts;
using Microsoft.Extensions.Logging;

namespace EventRegistry.Messaging.Publishing;

public class ServiceBusEventsQueuePublisher(
    ILogger<ServiceBusEventsQueuePublisher> logger,
    ServiceBusClient client
) : ServiceBusPublisher(DestinationName, client), IEventsQueuePublisher
{
    private const string DestinationName = "events-queue";

    public async Task PublishEventAsync(EventMessage message, CancellationToken ct = default)
    {
        var sbMessage = new ServiceBusMessage(JsonSerializer.Serialize(message))
        {
            ContentType = "application/json",
        };

        await Sender.SendMessageAsync(sbMessage, ct);
        logger.LogInformation("Published message to {Destination}", DestinationName);
    }
}
