using Data;
using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetByIdAsync(int id);

    public Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

    public Task UpdateAsync(TEntity entity);
}