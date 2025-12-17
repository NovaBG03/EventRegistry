namespace EventRegistry.Data.Entities;

public class Application : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ApiKeyHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
