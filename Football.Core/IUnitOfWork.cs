

namespace Football.Core;

public interface IUnitOfWork:IDisposable
{
    IBaseService<Club> Clubs { get; }
    IBaseService<Player> Players { get; }
    IBaseService<League> Leagues { get; }
    Task<int> Complete();
}
