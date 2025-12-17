using Azure.Messaging.ServiceBus;

namespace EventRegistry.Messaging.Publishing;

public abstract class ServiceBusPublisher(string destinationName, ServiceBusClient client) : IAsyncDisposable
{
    protected ServiceBusSender Sender { get; } = client.CreateSender(destinationName);

    public async ValueTask DisposeAsync()
    {
        await Sender.DisposeAsync();
    }
}
