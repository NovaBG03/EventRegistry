namespace EventRegistry.Business.Services.Contracts;

public interface IApplicationService
{
    Task<(Guid ApplicationId, string ApiKey)> RegisterAsync(string name, string? description);
}
