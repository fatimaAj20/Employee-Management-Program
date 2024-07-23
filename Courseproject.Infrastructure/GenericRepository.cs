

using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Courseproject.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private ApplicationDbContext applicationDbContext;
    private DbSet<T> DbSet { get; }
    public GenericRepository(ApplicationDbContext context)
    {
        applicationDbContext = context;
        DbSet = applicationDbContext.Set<T>();
    }
    public void Delete(T entity)
    {
        if(applicationDbContext.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
           
        }
        DbSet.Remove(entity);

    }
    
    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);
        
        if(take != null)
            query = query.Take(take.Value);
        
        return await query .ToListAsync();
       
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        query = query.Where(e => e.Id == id);

        foreach (var include in includes)
            query = query.Include(include);

        return await query.SingleOrDefaultAsync();

    }

    public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var filter in filters)
            query = query.Where(filter);

        foreach (var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();

    }

    public async Task<int> InsertAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task SaveChangesAsync()
    { 
       await applicationDbContext.SaveChangesAsync();
    }

    public void Update(T enitity)
    {
        DbSet.Attach(enitity);
        applicationDbContext.Entry(enitity).State = EntityState.Modified;
    }
}
