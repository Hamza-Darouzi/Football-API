

namespace Football.Application.Features.Leagues.Commands.Delete;

public class LeaguesDeleteRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaguesDeleteRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(LeaguesDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = await _unitOfWork.Leagues.GetOneAsync(l => l.Id == request.LeagueId)
                                                .FirstOrDefaultAsync(cancellationToken);
           
            if (league is null)
                return new Result(false, Error.NullValue);

            _unitOfWork.Leagues.Remove(league);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}
