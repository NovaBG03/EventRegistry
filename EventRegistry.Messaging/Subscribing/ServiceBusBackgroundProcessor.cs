using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace EventRegistry.Messaging.Subscribing;

public abstract class ServiceBusBackgroundProcessor : BackgroundService
{
    private readonly ServiceBusProcessor _processor;

    protected ServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string queueName)
    {
        _processor = client.CreateProcessor(queueName);
    }

    protected ServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string queueName,
        ServiceBusProcessorOptions options)
    {
        _processor = client.CreateProcessor(queueName, options);
    }

    protected ServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string topicName,
        string subscriptionName)
    {
        _processor = client.CreateProcessor(topicName, subscriptionName);
    }

    protected ServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string topicName,
        string subscriptionName,
        ServiceBusProcessorOptions options)
    {
        _processor = client.CreateProcessor(topicName, subscriptionName, options);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += ProcessMessageAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;

        await _processor.StartProcessingAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    protected abstract Task ProcessMessageAsync(ProcessMessageEventArgs args);

    protected abstract Task ProcessErrorAsync(ProcessErrorEventArgs args);
}
