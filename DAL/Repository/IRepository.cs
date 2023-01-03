using System.Linq.Expressions;


namespace DAL.Repositry;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<int> SaveChangesAsync();
}