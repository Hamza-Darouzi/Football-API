

namespace Football.Infrastructure.Repository.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context,
                     UserManager<User> userManager)

    {
        _context = context;
        Users = new UserRepository<User>(userManager, context);
        Clubs = new BaseRepository<Club>(context);
        Leagues = new BaseRepository<League>(context);
        Players = new BaseRepository<Player>(context);
        RefreshTokens = new BaseRepository<RefreshToken>(context);
    }
    public IUserRepository<User> Users { get; }
    public IBaseRepository<Club> Clubs { get; }
    public IBaseRepository<League> Leagues { get; }
    public IBaseRepository<Player> Players { get; }
    public IBaseRepository<RefreshToken> RefreshTokens { get; }

   
    public async void Dispose()
    {
        await _context.DisposeAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
