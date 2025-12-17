using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace EventRegistry.Messaging.Subscribing;

public abstract class JsonServiceBusBackgroundProcessor<T> : ServiceBusBackgroundProcessor
{
    protected JsonServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string queueName)
        : base(client, queueName)
    {
    }

    protected JsonServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string queueName,
        ServiceBusProcessorOptions options)
        : base(client, queueName, options)
    {
    }

    protected JsonServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string topicName,
        string subscriptionName)
        : base(client, topicName, subscriptionName)
    {
    }

    protected JsonServiceBusBackgroundProcessor(
        ServiceBusClient client,
        string topicName,
        string subscriptionName,
        ServiceBusProcessorOptions options)
        : base(client, topicName, subscriptionName, options)
    {
    }

    protected sealed override async Task ProcessMessageAsync(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var message = JsonSerializer.Deserialize<T>(body);
        if (message == null)
        {
            await args.AbandonMessageAsync(args.Message);
            return;
        }

        try
        {
            await ProcessObjectMessage(message);
        }
        finally
        {
            await args.CompleteMessageAsync(args.Message);
        }
    }

    protected abstract Task ProcessObjectMessage(T eventMessage);
}
