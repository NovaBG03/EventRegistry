using Azure.Messaging.ServiceBus;
using EventRegistry.Messaging.Messages;

namespace EventRegistry.Messaging.Subscribing;

public abstract class EventsQueueBackgroundProcessor(ServiceBusClient client)
    : JsonServiceBusBackgroundProcessor<EventMessage>(client, QueueName)
{
    private const string QueueName = "events-queue";
}
