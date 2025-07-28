namespace Football.Application.Features.Clubs.Commands.Delete;

public class ClubsDeleteRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<ClubsDeleteRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ClubsDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var club = await _unitOfWork.Clubs.GetOneAsync(l => l.Id == request.ClubId).FirstOrDefaultAsync(cancellationToken);
            if ( club is null)
                return new Result(false, Error.NullValue);

                 _unitOfWork.Clubs.Remove(club);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}
