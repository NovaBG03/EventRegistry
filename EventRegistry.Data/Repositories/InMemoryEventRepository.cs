using EventRegistry.Data.Entities;
using EventRegistry.Data.Repositories.Contracts;

namespace EventRegistry.Data.Repositories;

public class InMemoryEventRepository : InMemoryRepository<Event>, IEventRepository
{
}
