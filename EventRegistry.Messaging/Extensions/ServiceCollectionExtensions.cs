using EventRegistry.Messaging.Publishing;
using EventRegistry.Messaging.Publishing.Contracts;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistry.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceBusMessaging(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(builder => builder.AddServiceBusClient(connectionString));

        services.AddSingleton<IEventsQueuePublisher, ServiceBusEventsQueuePublisher>();
        services.AddSingleton<INotificationsTopicPublisher, ServiceBusNotificationsTopicPublisher>();

        return services;
    }
}
