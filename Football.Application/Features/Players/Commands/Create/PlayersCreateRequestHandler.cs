
namespace Football.Application.Features.Players.Commands.Create;

public class PlayersCreateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<PlayersCreateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(PlayersCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var club = await _unitOfWork.Clubs.GetOneAsync(l => l.Id == request.ClubId).FirstOrDefaultAsync(cancellationToken);
            if (club is null)
                return new Result(false, Error.NullValue);

            var player = new Player(request.ClubId,request.Name, request.Nation, request.BirthYear);

            if (request.Image is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.Image, "Players");
                if (uploadResult.error == Error.None)
                    player.Image = uploadResult.data.ToString();
            }
            await _unitOfWork.Players.AddAsync(player);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}
