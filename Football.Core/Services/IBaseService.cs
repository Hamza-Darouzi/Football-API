
namespace Football.Core.Services;

public interface IBaseService<T> where T : class
{
    public Task Create(T item);
    public void Delete(T item);
    public void Update(T item);
    public Task<T> FindAsync(Expression<Func<T, bool>> match);
    public Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null!);

    public Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null!);
    public Task<ICollection<T>> GetAllAsync();
    public  ICollection<T> GetAllOrdered(Func<T, string> match);
    public Task<T> GetById(int id);
  
 }
