
namespace Football.Domain.Services.Players;

public class PlayersService(IUnitOfWork unit, IMapper mapper) : IPlayersService
{
    private readonly IUnitOfWork _unit = unit;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> GetAll(int page,int size)
    {
        var players = await _unit.Players.GetAllNAsync(["Club"]);

        if (players is null)
            return new Result(null, "No Content");

        var response = players.Paginate(page, size);
        return new Result(response, "Done");

    }
    public async Task<Result> Get(int id)
    {
        var player = await _unit.Players.FindNAsync(x => x.Id == id, ["Club"]);

        if (player is null) 
            return new Result(null,"Invalid Id");

        return new Result(player, "Done");
    }
    public async Task<Result> Create(CreatePlayerDTO model)
    {
        if (model is null)
            return new Result(false, "Invalid Input");

        var player = _mapper.Map<Player>(model);
        var club = await _unit.Clubs.GetById(model.clubId);
        if(club is null)
            return new Result(false, "Invalid Club");
        await _unit.Players.Create(player);
        return new Result(true, "Done");
    }
    public async Task<Result> Update(UpdatePlayerDTO model)
    {
        var player = await _unit.Players.GetById(model.id);
        if (player is null)
            return new Result(false, "Invalid Input");

        player.Name = model.name;
        player.BirthYear = model.birthYear;
        player.ClubId = model.clubId;
        player.Nation = model.nation;
        await _unit.Players.Update(player);
        return new Result(true, "Done");
    }

    public async Task<Result> Delete(int id)
    {
        var player = await _unit.Players.GetById(id);
        if (player is null) 
            return new Result(false , "Invalid Id");
        await _unit.Players.Delete(player);
        return new Result(true,"Done");
    }
}
