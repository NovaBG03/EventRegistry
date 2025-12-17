namespace EventRegistry.Data.Entities;

public class Event : EntityBase
{
    public Guid ApplicationId { get; set; }
    public EventCategory Category { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
