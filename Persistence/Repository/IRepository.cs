namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetById(int id);
}