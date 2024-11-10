using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task SaveAsync();
    Task RemoveAsync(T entity);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool tracked = true);
}