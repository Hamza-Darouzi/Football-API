

namespace Football.Domain.Services.Clubs;

public class ClubService(IUnitOfWork unit, IMapper mapper) : IClubService
{
    private readonly IUnitOfWork _unit = unit;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> GetAll(int page , int size)
    {
        var clubs = await _unit.Clubs.GetAllNAsync(["League"]);
        if (clubs.Count == 0) 
                return new Result(null,"No Content");
        var response = clubs.Paginate(page, size);

        return new Result(response, "Done");
    }

    public async Task<Result> Get(int id)
    {
        var club = await _unit.Clubs.FindNAsync(x => x.Id == id, new[] { "Players","League" });

        if (club is null)
            return new Result(null, "Invalid Id");

        return new Result(club, "Done");

    }

    public async Task<Result> Create(CreateClubDTO model)
    {
        if (model is null)
            return new Result(false, "Invalid Input");

        var club = _mapper.Map<Club>(model);
        await _unit.Clubs.Create(club);
        return  new Result(true, "Done");
    }
 
    public async Task<Result> Delete(int id)
    {
        var club = await _unit.Clubs.GetById(id);
        if (club is null)
            return new Result(false, "Invalid Id");

        await _unit.Clubs.Delete(club);
        return new Result(true, "Done");
    }

    public async Task<Result> Update(UpdateClubDTO model)
    {
        var club = await _unit.Clubs.GetById(model.id);
        if (club is null)
            return new Result(false, "Invalid Id");
        club.Name = model.name;
        club.LeagueId = model.leagueId;
        club.FoundingDate = model.foundingDate;

        await _unit.Clubs.Update(club);
        return new Result(true, "Done");
    }
}