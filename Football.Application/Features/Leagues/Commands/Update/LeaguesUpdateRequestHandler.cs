

namespace Football.Application.Features.Leagues.Commands.Update;

public class LeaguesUpdateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<LeaguesUpdateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(LeaguesUpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = await _unitOfWork.Leagues.GetOneAsync(l => l.Id == request.LeagueId)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (league is null)
                return new Result(false, Error.NullValue);

            if (request.Logo is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.Logo, "Leagues");
                if (uploadResult.error == Error.None)
                    league.Logo = uploadResult.data.ToString();
            }

            league.Name = request.Name;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}

