using EventRegistry.Data.Entities;
using EventRegistry.Data.Repositories.Contracts;

namespace EventRegistry.Data.Repositories;

public class InMemoryApplicationRepository : InMemoryRepository<Application>, IApplicationRepository
{
    public Task<Application?> GetByApiKeyAsync(string apiKey)
    {
        var application = Entities.Values.FirstOrDefault(a => a.ApiKeyHash == apiKey);
        return Task.FromResult(application);
    }
}
