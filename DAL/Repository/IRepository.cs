using System.Linq.Expressions;
using Data;
using System.Linq.Expressions;

namespace DAL.Repositry;

public interface IRepository<TEntity>
{
    public Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<int> SaveChangesAsync();
}