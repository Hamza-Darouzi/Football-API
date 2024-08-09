


namespace Football.Domain.Services.Leagues;

public class LeagueService(IUnitOfWork unit, IMapper mapper) : ILeagueService
{
    private readonly IUnitOfWork _unit = unit;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Create(CreateLeagueDTO model)
    {
        if (model is null) 
            return new Result(false,"Invalid Input");

        var league = _mapper.Map<League>(model);
        await _unit.Leagues.Create(league);

        return new Result(true, "Done");
    }

    public async Task<Result> Delete(int id)
    {
        var league = await _unit.Leagues.FindAsync(x => x.Id == id);
        if (league is null) 
            return new Result(false,"Invalid Id");

        await _unit.Leagues.Delete(league);
        return new Result(true, "Done");

    }

    public async Task<Result> GetAll(int page,int size)
    {
        var leagues = await _unit.Leagues.GetAllNAsync(["Clubs"]);

        if (leagues.Count == 0) 
            return new Result (null,"Done");
        var response = leagues.Paginate(page, size);
        return new Result(response, "Done");
    }

    public async Task<Result> Get(int id)
    {
        var league = await _unit.Leagues.FindNAsync(x => x.Id == id, new[] { "Clubs" });
        if (league is null)
            return new Result(null, "Invalid Id");
        return new Result(league, "Done");
    }

    public async Task<Result> Update(UpdateLeagueDTO model)
    {
        var league = await _unit.Leagues.FindAsync(x => x.Id == model.id);
        if (league is null)
            return new Result(false, "Invalid Id");

        league.Name = model.name;
        await _unit.Leagues.Update(league);

           return new Result(true, "Invalid Id");

    }
}