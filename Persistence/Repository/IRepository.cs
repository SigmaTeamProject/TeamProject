namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    public Task<TEntity> FirstOrDefault(Predicate<TEntity> predicate);
}