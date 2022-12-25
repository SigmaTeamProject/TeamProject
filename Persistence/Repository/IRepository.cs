using Data;

namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    public Task<TEntity> FirstOrDefault(Predicate<TEntity> predicate);
    public Task<TEntity> GetById(int id);
    public Task<TEntity> Register(TEntity entity);
}