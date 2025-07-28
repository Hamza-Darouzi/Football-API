

namespace Football.Application.Features.Players.Commands.Update;
public class PlayersUpdateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<PlayersUpdateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(PlayersUpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _unitOfWork.Players.GetOneAsync(l => l.Id == request.playerId).FirstOrDefaultAsync(cancellationToken);
            if (player is null)
                return new Result(false, Error.NullValue);

            if (request.Image is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.Image, "Players");
                if (uploadResult.error == Error.None)
                    player.Image = uploadResult.data.ToString();
            }
            player.ClubId = request.ClubId;
            player.Name = request.Name;
            player.BirthDay = request.BirthYear;
            player.Nation = request.Nation;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}

