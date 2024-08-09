


namespace Football.Domain.UOW;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseRepository <Club>   Clubs   { get; private set; }
    public IBaseRepository <Player> Players { get; private set; }
    public IBaseRepository <League> Leagues { get; private set; }
    public IBaseRepository <User> Users { get; private set; }
    public IBaseRepository <RefreshToken> RefreshTokens { get; private set; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Clubs = new BaseRepository<Club>(_context);
        Players = new BaseRepository<Player>(_context);
        Leagues = new BaseRepository<League>(_context);
        Users = new BaseRepository<User>(_context);
        RefreshTokens = new BaseRepository<RefreshToken>(_context);
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
