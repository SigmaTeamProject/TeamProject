using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DAL.Context;
using DAL.Repositry;

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

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = dbSet;

        return await query.ToListAsync();
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