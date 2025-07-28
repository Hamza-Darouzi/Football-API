

namespace Football.Application.Features.Leagues.Commands.Create;

public class LeaguesCreateRequestHandler(IUnitOfWork unitOfWork,IFileService fileService) : IRequestHandler<LeaguesCreateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Result> Handle(LeaguesCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var league = new League(request.Name);
            if (request.Logo is not null)
            {
                var uploadResult = await _fileService.UploadImageAsyncV3(request.Logo, "Leagues");
                if (uploadResult.error == Error.None)
                    league.Logo = uploadResult.data.ToString();
            }
            await _unitOfWork.Leagues.AddAsync(league);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("400", ex.Message));
        }
    }
}
