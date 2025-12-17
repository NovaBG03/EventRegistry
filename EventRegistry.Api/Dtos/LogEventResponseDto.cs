using System.ComponentModel;

namespace EventRegistry.Api.Dtos;

public class LogEventResponseDto
{
    [Description("The unique identifier assigned to the queued event")]
    public Guid EventId { get; set; }

    [Description("The current status of the event (e.g., queued, processing, completed)")]
    public string Status { get; set; } = string.Empty;
}
