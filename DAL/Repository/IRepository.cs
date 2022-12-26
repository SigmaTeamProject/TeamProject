using Data;

namespace DAL.Repositry;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetById(int id);
}