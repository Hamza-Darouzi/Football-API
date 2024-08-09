


namespace Football.EF;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseService <Club>   Clubs   { get; private set; }
    public IBaseService <Player> Players { get; private set; }
    public IBaseService <League> Leagues { get; private set; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Clubs = new BaseRepository<Club>(_context);
        Players = new BaseRepository<Player>(_context);
        Leagues = new BaseRepository<League>(_context);
    }

    public async Task<int>  Complete()
    {
       return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
  

}
