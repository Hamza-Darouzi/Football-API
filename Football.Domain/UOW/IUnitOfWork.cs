


namespace Football.Domain.UOW;

public interface IUnitOfWork:IDisposable
{
    IBaseRepository<Club> Clubs { get; }
    IBaseRepository<Player> Players { get; }
    IBaseRepository<League> Leagues { get; }
    IBaseRepository<User> Users { get; }
    IBaseRepository<RefreshToken> RefreshTokens { get; }
    Task<int> Complete();
}
