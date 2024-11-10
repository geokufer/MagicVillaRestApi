using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository;

public class Repository <T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _applicationDbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        await _applicationDbContext.AddAsync(entity);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        //query executes here
        return await query.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        query = tracked ? query.AsTracking() : query.AsNoTracking();
        
        query = query.Where(filter);
        
        return await query.FirstOrDefaultAsync();
    }
}