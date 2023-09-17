
namespace FootballAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class PlayerController: ControllerBase
{
    private readonly IUnitOfWork _unit;
    public PlayerController(IUnitOfWork unit)
    {
        _unit = unit;
    }
    [HttpGet]
    [Route("GetAllPlayers")]
    public async Task<IActionResult> GetAll()
    {
        var players = await _unit.Players.GetAllAsync();
        if(players is null ) return NotFound();
        return Ok(players);
    }

    [HttpGet]
    [Route("GetPlayer")]
    public async Task<IActionResult> GetById(int id)
    {
        var player = await _unit.Players.FindAsync(x => x.Id == id, new[] { "Club" });
        if(player is null ) return NotFound();
        return Ok(player);
    }

    [HttpPost]
    [Route("AddPlayer")]
    public async Task<ActionResult> Add(PlayerDTO model)
    {
        if(model is  null) return NotFound();
        
        var player = new Player()
        {
            Name=model.Name,
            BirthYear = model.BirthYear,
            ClubId = model.ClubId
        };
         var club = await _unit.Clubs.FindAsync(x=>x.Id==model.ClubId);
         club.Players.Add(player);
         await _unit.Players.Create(player);
         await _unit.Complete();
        return Ok(player);
    }
    
    [HttpPut]
    [Route("UpdatePlayer")]
    public async Task<IActionResult> Update(Player model)
    {
        var player = await _unit.Players.FindAsync(x=>x.Id==model.Id);
        if(player is null ) return NotFound();
        player.Name=model.Name;
        player.BirthYear=model.BirthYear;
        player.ClubId = model.ClubId;
        player.Nation = model.Nation;
        _unit.Players.Update(player);
        await _unit.Complete();
        return Ok();
    }
    
    [HttpDelete]
    [Route("DeletePlayer")]
    public async Task<IActionResult> Delete(int id)
    {
        var player =  await _unit.Players.FindAsync(x=>x.Id==id);
        if(player is null ) return NotFound();
        _unit.Players.Delete(player);
        await _unit.Complete();
        return Ok();
    }
    
}
