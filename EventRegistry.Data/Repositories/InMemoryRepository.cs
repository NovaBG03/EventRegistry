using System.Collections.Concurrent;
using EventRegistry.Data.Entities.Contracts;
using EventRegistry.Data.Repositories.Contracts;

namespace EventRegistry.Data.Repositories;

public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected ConcurrentDictionary<Guid, TEntity> Entities { get; } = new();

    public Task<TEntity?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(Entities.GetValueOrDefault(id));
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TEntity>>(Entities.Values.ToList());
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.CreateVersion7();
        }

        return Entities.TryAdd(entity.Id, entity)
            ? Task.FromResult(entity)
            : throw new InvalidOperationException($"Entity {nameof(TEntity)} with id '{entity.Id}' already exists");
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Task.FromResult(Entities.TryRemove(id, out _));
    }
}
