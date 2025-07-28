


namespace Football.Infrastructure.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public virtual IQueryable<T> GetAll(bool noTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual IQueryable<T> GetOneAsync(
        Expression<Func<T, bool>> predicate,
        bool noTracking = false
    )
    {
        var query = _context.Set<T>().AsQueryable();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        var item = query.Where(predicate);

        return item;
    }

    public virtual IQueryable<T> Find(
        Expression<Func<T, bool>> predicate,
        bool noTracking = false
    )
    {
        var query = GetAll().Where(predicate);

        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public virtual bool Update(T entity)
    {
        try
        {
            _context.Update(entity);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public virtual bool UpdateRange(IEnumerable<T> entities)
    {
        try
        {
            foreach (var entity in entities)
            {
                _context.Update(entity);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public virtual bool Remove(T entity)
    {
        try
        {
            _context.Remove(entity);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            await _context.AddRangeAsync(entities);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public virtual bool RemoveRange(IEnumerable<T> entities)
    {
        try
        {
            _context.RemoveRange(entities);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> Exist(Expression<Func<T, bool>> predicate)
    {
        try
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
        catch
        {
            return false;
        }
    }
}
