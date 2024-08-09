
#nullable disable

namespace Football.Domain.Repository;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context = context;
    public async Task Create(T item)
    {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(T item)
    {
        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
    }
    public async Task Update(T entity)
    {
       _context.Entry(entity).State =  EntityState.Modified;
        await _context.SaveChangesAsync();

    }
    public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        => await _context.Set<T>()
                         .FirstOrDefaultAsync(match);
    public async Task<T> FindNAsync(Expression<Func<T, bool>> match)
        => await _context.Set<T>()
                         .AsNoTracking()
                         .FirstOrDefaultAsync(match);
    public async Task<T> GetById(int id)
      => await _context.Set<T>().FindAsync(id);
    public async Task<bool> Exist(Expression<Func<T,bool>> match)
      => await _context.Set<T>().AnyAsync(match);
    public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(match);
    }
    public async Task<T> FindNAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>()
                                      .AsNoTracking();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(match);
    }
    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.Where(match)
                          .ToListAsync();
    }
    public async Task<ICollection<T>> GetAllAsync( string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.ToListAsync();
    }
    public async Task<ICollection<T>> GetAllNAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>()
                                      .AsNoTracking();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.Where(match).ToListAsync();
    }
    public async Task<ICollection<T>> GetAllNAsync(string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>()
                                      .AsNoTracking();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.ToListAsync();
    }
    public async Task<ICollection<T>> GetAllAsync()
      => await _context.Set<T>()
                       .Select(x => x)
                       .ToListAsync();

    public async Task<ICollection<T>> GetAllNAsync()
      => await _context.Set<T>()
                   .AsNoTracking()
                   .Select(x => x)
                   .ToListAsync();

    public ICollection<T> GetAllNOrdered(Func<T, string> match)
          =>  _context.Set<T>()
                      .AsNoTracking()
                      .Select(x => x)
                      .OrderBy(match)
                      .ToList();
   
}
