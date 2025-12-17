using EventRegistry.Messaging.Extensions;
using EventRegistry.NotificationService.Workers;

namespace EventRegistry.NotificationService;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddServiceBusMessaging(
            builder.Configuration.GetConnectionString("ServiceBus") ??
            throw new InvalidOperationException("Connection string 'ServiceBus' is not configured.")
        );

        builder.Services.AddHostedService<ConsoleNotificationWorker>();

        var host = builder.Build();
        host.Run();
    }
}
