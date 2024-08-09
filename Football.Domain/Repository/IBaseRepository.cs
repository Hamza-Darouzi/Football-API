

namespace Football.Domain.Repository;

public interface IBaseRepository<T> where T : class
{
    public Task Create(T item);
    public Task Delete(T item);
    public Task Update(T item);
    public Task<T> FindAsync(Expression<Func<T, bool>> match);
    public Task<T> FindNAsync(Expression<Func<T, bool>> match);
    public Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null!);
    public Task<T> FindNAsync(Expression<Func<T, bool>> match, string[] includes = null!);
    public Task<bool> Exist(Expression<Func<T, bool>> match);
    public Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null!);
    public Task<ICollection<T>> GetAllNAsync(Expression<Func<T, bool>> match, string[] includes = null!);
    public Task<ICollection<T>> GetAllAsync();
    public Task<ICollection<T>> GetAllNAsync();
    public Task<ICollection<T>> GetAllAsync(string[] includes = null!);
    public Task<ICollection<T>> GetAllNAsync(string[] includes = null!);
    public  ICollection<T> GetAllNOrdered(Func<T, string> match);
    public Task<T> GetById(int id);
  
 }
