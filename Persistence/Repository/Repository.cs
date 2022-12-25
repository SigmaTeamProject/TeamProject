namespace Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity>
{
    public Task<TEntity> FirstOrDefault(Predicate<TEntity> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Register(TEntity entity)
    {
        throw new NotImplementedException();
    }
}