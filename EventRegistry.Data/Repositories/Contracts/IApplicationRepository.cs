using EventRegistry.Data.Entities;

namespace EventRegistry.Data.Repositories.Contracts;

public interface IApplicationRepository : IRepository<Application>
{
    Task<Application?> GetByApiKeyAsync(string apiKey);
}
