using EventRegistry.Data.Entities.Contracts;

namespace EventRegistry.Data.Entities;

public class EntityBase : IEntity
{
    public Guid Id { get; set; }
}
