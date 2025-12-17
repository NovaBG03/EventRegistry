using EventRegistry.Business.Security.Contracts;
using EventRegistry.Business.Services.Contracts;
using EventRegistry.Data.Entities;
using EventRegistry.Data.Repositories.Contracts;

namespace EventRegistry.Business.Services;

public class ApplicationService(
    IApplicationRepository applicationRepository,
    ISecretHasher secretHasher)
    : IApplicationService
{
    public async Task<(Guid ApplicationId, string ApiKey)> RegisterAsync(string name, string? description)
    {
        var apiKey = Guid.CreateVersion7().ToString();
        var apiKeyHash = secretHasher.Hash(apiKey);

        var application = await applicationRepository.AddAsync(new Application
        {
            Name = name,
            Description = description,
            ApiKeyHash = apiKeyHash,
            CreatedAt = DateTime.UtcNow
        });

        return (application.Id, apiKey);
    }
}
