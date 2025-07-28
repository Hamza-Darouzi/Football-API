

namespace Football.Application.Features.Leagues.Queries.Get;

public class LeaguesGetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaguesGetRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(LeaguesGetRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = await _unitOfWork.Leagues.GetOneAsync(c => c.Id == request.LeagueId)
                                                  .Select(l => new LeaguesGetDTO(
                                                      l.Id,
                                                      l.Name,
                                                      l.Logo,
                                                      l.Clubs.Select(c => new LeagueClubsGetDTO(
                                                          c.Id,
                                                          c.Name,
                                                          c.Logo
                                                      )).ToList()
                                                  )).FirstOrDefaultAsync(cancellationToken);

            if (league is null)
                return new Result(null, Error.NullValue);

            return new Result(league, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
}

