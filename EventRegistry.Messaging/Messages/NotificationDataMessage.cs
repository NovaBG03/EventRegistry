namespace EventRegistry.Messaging.Messages;

public class NotificationDataMessage
{
    public Guid EventId { get; set; }
    public Guid ApplicationId { get; set; }
    public required string Category { get; set; }
    public required string Message { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
