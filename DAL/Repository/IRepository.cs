using Data;
using System.Linq.Expressions;

namespace DAL.Repositry;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetByIdAsync(int id);

    public Task<IEnumerable<TEntity>> GetAllAsync();

    public Task UpdateAsync(TEntity entity);
}