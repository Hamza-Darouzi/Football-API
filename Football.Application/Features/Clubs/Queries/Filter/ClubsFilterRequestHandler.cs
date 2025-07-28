
namespace Football.Application.Features.Clubs.Queries.Filter;

public class ClubsFilterRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<ClubsFilterRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ClubsFilterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query =  _unitOfWork.Clubs.GetAll(true);
            var clubs = await Filter(query,request.Name,request.before,request.after)
                       .Select(c => new ClubsFilterDTO(
                           c.Id,
                           c.Name,
                           c.LeagueId,
                           c.League.Name,
                           c.Logo
                       )).PaginateAsync(request.page,request.size);
            if (clubs is null)
                return new Result(null, Error.NullValue);
          
            return new Result(clubs, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(null, new Error("400", ex.Message));
        }
    }
    public IQueryable<Club> Filter( IQueryable<Club> clubs,string? name,DateTime? before , DateTime? after)
    {
        if (before is not null)
            clubs = clubs = clubs.Where(c => c.FoundingDate <= before);
        if (after is not null)
            clubs = clubs = clubs.Where(c => c.FoundingDate >= after);
        if (name is not null)
            clubs=clubs.Where(c => c.Name.ToLower().Contains(name.ToLower()));
        return clubs;
    }
}

