using EventRegistry.Business.Extensions;
using EventRegistry.Data.Extensions;
using EventRegistry.EventProcessor.Services;
using EventRegistry.EventProcessor.Services.Contracts;
using EventRegistry.EventProcessor.Workers;
using EventRegistry.Messaging.Extensions;

namespace EventRegistry.EventProcessor;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddBusinessServices();
        builder.Services.AddInMemoryRepositories();

        builder.Services.AddSingleton<IEventProcessorService, EventProcessorService>();

        builder.Services.AddServiceBusMessaging(
            builder.Configuration.GetConnectionString("ServiceBus") ??
            throw new InvalidOperationException("Connection string 'ServiceBus' is not configured.")
        );

        builder.Services.AddHostedService<EventProcessorWorker>();

        var host = builder.Build();
        host.Run();
    }
}
