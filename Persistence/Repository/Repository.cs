using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public ApplicationDbContext context;
    public DbSet<TEntity> dbSet;

    public Repository(ApplicationDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}