using System.Linq.Expressions;
using Data;
using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetByIdAsync(int id);
    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<int> SaveChangesAsync();
}