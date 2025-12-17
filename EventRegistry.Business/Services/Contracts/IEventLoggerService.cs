namespace EventRegistry.Business.Services.Contracts;

public interface IEventLoggerService
{
    Task<(Guid EventId, string Status)> LogEventAsync(
        Guid applicationId,
        string category,
        string message,
        DateTimeOffset timestamp,
        Dictionary<string, object>? metadata);
}
