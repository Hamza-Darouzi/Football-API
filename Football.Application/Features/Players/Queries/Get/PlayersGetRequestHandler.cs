
namespace Football.Application.Features.Players.Queries.Get;

public class PlayersGetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<PlayersGetRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(PlayersGetRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _unitOfWork.Players.GetOneAsync(c => c.Id == request.playerId)
                                                  .Select(p => new PlayersGetDTO(
                                                      p.Id,
                                                      p.Name,
                                                      p.Nation,
                                                      p.ClubId,
                                                      p.Club.Name,
                                                      p.BirthDay,
                                                      p.Image
                                                  )).FirstOrDefaultAsync(cancellationToken);

            if (player is null)
                return new Result(null, Error.NullValue);

            return new Result(player, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
}

