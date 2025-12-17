using EventRegistry.Data.Repositories;
using EventRegistry.Data.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistry.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationRepository, InMemoryApplicationRepository>();
        services.AddSingleton<IEventRepository, InMemoryEventRepository>();

        return services;
    }
}
