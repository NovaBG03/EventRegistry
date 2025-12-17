using Azure.Messaging.ServiceBus;
using EventRegistry.EventProcessor.Services.Contracts;
using EventRegistry.Messaging.Messages;
using EventRegistry.Messaging.Subscribing;

namespace EventRegistry.EventProcessor.Workers;

public class EventProcessorWorker(
    ILogger<EventProcessorWorker> logger,
    ServiceBusClient client,
    IEventProcessorService eventProcessorService
) : EventsQueueBackgroundProcessor(client)
{
    protected override async Task ProcessObjectMessage(EventMessage eventMessage)
    {
        logger.LogInformation("Processing EventMessage for event {Id}", eventMessage.EventId);
        await eventProcessorService.ProcessEventMessageAsync(eventMessage);
    }

    protected override Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        logger.LogWarning(
            args.Exception,
            "Service Bus error in {Source}. Entity: {EntityPath}, Namespace: {Namespace}, Reason: {ErrorSource}",
            nameof(EventProcessorWorker),
            args.EntityPath,
            args.FullyQualifiedNamespace,
            args.ErrorSource
        );
        return Task.CompletedTask;
    }
}
