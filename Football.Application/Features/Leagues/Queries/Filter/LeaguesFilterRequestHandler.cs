

namespace Football.Application.Features.Leagues.Queries.Filter;

public class LeaguesFilterRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaguesFilterRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(LeaguesFilterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _unitOfWork.Leagues.GetAll(true);
            var leagues = Filter(query, request.Name).Select(l => new LeaguesFilterDTO(
                l.Id,
                l.Name,
                l.Logo
            )).PaginateAsync(request.page,request.size);
            if (leagues is null)
                return new Result(null, Error.NullValue);

            return new Result(leagues, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
    public IQueryable<League> Filter(IQueryable<League> leagues, string? name)
    {
        if (name is not null)
            leagues = leagues.Where(c => c.Name.ToLower().Contains(name.ToLower()));

        return leagues;
    }
}

