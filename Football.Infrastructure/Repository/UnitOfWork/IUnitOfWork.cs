


namespace Football.Infrastructure.Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository<User> Users { get; }
    IBaseRepository<Club> Clubs { get; }
    IBaseRepository<League> Leagues { get; }
    IBaseRepository<Player> Players { get; }
    IBaseRepository<RefreshToken> RefreshTokens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
