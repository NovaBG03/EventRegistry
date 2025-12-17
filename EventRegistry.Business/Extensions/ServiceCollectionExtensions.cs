using EventRegistry.Business.Security;
using EventRegistry.Business.Security.Contracts;
using EventRegistry.Business.Services;
using EventRegistry.Business.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistry.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddSingleton<ISecretHasher, Sha256SecretHasher>();

        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IEventLoggerService, EventLoggerService>();

        return services;
    }
}
