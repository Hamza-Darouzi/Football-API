
namespace Football.Application.Features.Clubs.Commands.Create;

public class ClubsCreateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<ClubsCreateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(ClubsCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = await _unitOfWork.Leagues.GetOneAsync(l => l.Id == request.LeagueId).FirstOrDefaultAsync(cancellationToken);
            if (league is null)
                return new Result(false, Error.NullValue);

            var club = new Club(request.Name, request.FoundingDate, request.LeagueId);
            if (request.logo is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.logo,"Clubs");
                if (uploadResult.error == Error.None)
                    club.Logo = uploadResult.data.ToString();
            }
            await _unitOfWork.Clubs.AddAsync(club);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch(Exception ex)
        {
            return new Result(false, new Error("400",ex.Message));
        }
    }
}
