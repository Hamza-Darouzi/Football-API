using System.Linq.Expressions;

namespace Football.Infrastructure.Repository.Base;

public interface IBaseRepository<T>
    where T : class
{
    IQueryable<T> GetAll(bool noTracking = false);
    Task<bool> Exist(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetOneAsync(Expression<Func<T, bool>> predicate, bool noTracking = false);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool noTracking = false);
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    bool Update(T entity);
    bool UpdateRange(IEnumerable<T> entities);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entities);
}
