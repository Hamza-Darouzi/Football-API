

namespace Football.Application.Features.Clubs.Commands.Update;

public class ClubsUpdateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<ClubsUpdateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(ClubsUpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = await _unitOfWork.Leagues.GetOneAsync(l => l.Id == request.LeagueId).FirstOrDefaultAsync(cancellationToken);
            var club = await _unitOfWork.Clubs.GetOneAsync(l => l.Id == request.ClubId).FirstOrDefaultAsync(cancellationToken);
            if (league is null || club is null)
                return new Result(false, Error.NullValue);
          
            club.LeagueId = request.LeagueId;
            club.Name = request.Name;
            club.FoundingDate = request.FoundingDate;
            if (request.Logo is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.Logo, "Clubs");
                if (uploadResult.error == Error.None)
                    club.Logo = uploadResult.data.ToString();
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}

