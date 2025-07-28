
namespace Football.Application.Features.Clubs.Queries.Get;

public class ClubsGetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<ClubsGetRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ClubsGetRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var club  = await _unitOfWork.Clubs.GetOneAsync(c=>c.Id==request.ClubId)
                                               .Select(c => new ClubGetDTO(
                                                   c.Id,
                                                   c.Name,
                                                   c.FoundingDate,
                                                   c.LeagueId,
                                                   c.League.Name,
                                                   c.Logo,
                                                   c.Players.Select(p => new ClubsPlayersGetDTO(
                                                       p.Id,
                                                       p.Name,
                                                       p.Image
                                                   )).ToList()
                                               )).FirstOrDefaultAsync(cancellationToken);
            
            if (club is null)
                return new Result(null, Error.NullValue);

            return new Result(club, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
}

