namespace DAL.Repositry;

public class Repository<TEntity> : IRepository<TEntity>
{
    public Task<TEntity> GetById(int id)
    {
        throw new NotImplementedException();
    }
}