using EventRegistry.Messaging.Messages;

namespace EventRegistry.EventProcessor.Services.Contracts;

public interface IEventProcessorService
{
    public Task ProcessEventMessageAsync(EventMessage eventMessage);
}
