

namespace Football.Application.Features.Players.Commands.Delete;

public class PlayersDeleteRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<PlayersDeleteRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(PlayersDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _unitOfWork.Players.GetOneAsync(l => l.Id == request.playerId).FirstOrDefaultAsync(cancellationToken);
            if (player is null)
                return new Result(false, Error.NullValue);

            _unitOfWork.Players.Remove(player);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}
