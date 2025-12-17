using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventRegistry.Api.Dtos;

public class LogEventRequestDto
{
    [Required(ErrorMessage = "Event category is required")]
    [RegularExpression("^(Debug|Info|Warning|Error|Critical)$",
        ErrorMessage = "Category must be Debug, Info, Warning, Error, or Critical")]
    [Description("The category of the event (e.g., Error, Warning, Info, Debug, Critical)")]
    public string? Category { get; set; }

    [Required(ErrorMessage = "Event message is required")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 1000 characters")]
    [Description("The main message describing the event")]
    public string? Message { get; set; }

    [Required(ErrorMessage = "Timestamp is required")]
    [Description("The UTC timestamp when the event occurred")]
    public DateTimeOffset? Timestamp { get; set; }

    [Description("Optional JSON payload with additional event data")]
    public Dictionary<string, object>? Metadata { get; set; }
}
