



namespace Football.Application.Features.Players.Queries.Filter;

public class PlayersFilterRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<PlayersFilterRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(PlayersFilterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _unitOfWork.Players.GetAll(true);
            var players = await Filter(query,request.ClubId, request.Name, request.before, request.after)
                       .Select(p => new PlayerFilterDTO(
                           p.Id,
                           p.ClubId,
                           p.Club.Name,
                           p.Name,
                           p.Nation,
                           (DateTime.UtcNow.Year - p.BirthDay.Year),
                           p.Image
                       )).PaginateAsync(request.page,request.size);
            if (players is null)
                return new Result(null, Error.NullValue);

            return new Result(players, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
    public IQueryable<Player> Filter(IQueryable<Player> players,int? clubId, string? name, DateTime? before, DateTime? after)
    {
        if (clubId is not null)
            players  = players.Where(c => c.ClubId == clubId);
        if (before is not null)
            players  = players.Where(c => c.BirthDay <= before);
        if (after is not null)
            players  = players.Where(c => c.BirthDay >= after);
        if (name is not null)
            players = players.Where(c => c.Name.ToLower().Contains(name.ToLower()));
        return players;
    }
}

