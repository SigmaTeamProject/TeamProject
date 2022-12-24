namespace Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity>
{
    public Task<TEntity> FirstOrDefault(Predicate<TEntity> predicate)
    {
        throw new NotImplementedException();
    }
}