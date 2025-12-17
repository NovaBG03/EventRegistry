using System.ComponentModel;

namespace EventRegistry.Api.Dtos;

public class RegisterApplicationResponseDto
{
    [Description("The unique identifier assigned to the registered application")]
    public Guid ApplicationId { get; set; }

    [Description("The API key for authentication. Store securely - cannot be retrieved again")]
    public required string ApiKey { get; set; }
}
