using EventRegistry.Data.Entities.Contracts;

namespace EventRegistry.Data.Repositories.Contracts;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}
