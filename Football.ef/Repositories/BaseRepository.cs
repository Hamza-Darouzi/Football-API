
namespace Fake_IMDB.EF.Repositories;

public class BaseRepository<T> : IBaseService<T> where T : class
{
    private readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task Create(T item)
    {
        await _context.Set<T>().AddAsync(item);
    }
    public void Delete(T item)
    {
        _context.Set<T>().Remove(item);
    }
    public void Update(T entity)
    {
       _context.Entry(entity).State =  EntityState.Modified;
    }
    public async Task<T> FindAsync(Expression<Func<T, bool>> match)
     => await _context.Set<T>().FirstOrDefaultAsync(match);
    public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(match);
    }
    public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null!)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.Where(match).ToListAsync();
    }
    public async Task<ICollection<T>> GetAllAsync()
      => await _context.Set<T>().Select(x => x).ToListAsync();
    public ICollection<T> GetAllOrdered(Func<T, string> match)
          =>  _context.Set<T>().Select(x => x).OrderBy(match).ToList();
    public async Task<T> GetById(int id)
       => await _context.Set<T>().FindAsync(id);

}
