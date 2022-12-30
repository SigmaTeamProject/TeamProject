using System.Linq.Expressions;
using DAL.Context;
using DAL.Repositry;
using Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id) ?? throw new ArgumentException("Entity with this id not found!");
    }

    public IQueryable<TEntity> Query(params Expression<Func<TEntity,object>>[] includes)
    {
        var dbSet = _dbContext.Set<TEntity>();
        var query = includes
            .Aggregate<Expression<Func<TEntity,object>>,IQueryable<TEntity>>(dbSet,(current,include) => current.Include(include));

        return query ?? dbSet;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        CheckNull(entity);
        return (await _dbSet.AddAsync(entity)).Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return await Task.Run(() => _dbSet.Update(entity).Entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                await _dbContext.Database.CurrentTransaction.RollbackAsync();
            }

            throw;
        }
    }

    private static void CheckNull(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity),"The entity to add cannot be null.");
        }
    }
}
