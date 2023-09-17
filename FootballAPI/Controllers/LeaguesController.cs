

namespace FootballAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class LeaguesController: ControllerBase
{
    private readonly IUnitOfWork _unit;

    public LeaguesController(IUnitOfWork unit)
    { 
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAllLeagues")]
    public async Task<IActionResult> GetAll()
    {
        var leagues = await _unit.Leagues.GetAllAsync();
        if(leagues.Count==0) return NoContent();
        return Ok(leagues);
    }
    
    [HttpGet]
    [Route("GetLeague")]
    public async Task<IActionResult> GetById(int id)
    {
        var league = await _unit.Leagues.FindAsync(x=>x.Id==id,new[]{"Clubs"});
        if(league is  null)
            return NotFound();
        return Ok(league);

    }

    [HttpPost]
    [Route("AddLeague")]
    public async Task<IActionResult> Add(LeagueDTO model)
    {
        if(model is null) return NotFound();
        var league = new League
        {
            Name=model.Name
        };
         await _unit.Leagues.Create(league);
         await _unit.Complete();
        return Ok(league);
    }
    
    [HttpDelete]
    [Route("DeleteLeague")]
    public async Task<IActionResult>Delete(int id)
    {
        var league = await _unit.Leagues.FindAsync(x=>x.Id==id);
        if(league is null ) return NotFound();
        
       _unit.Leagues.Delete(league);
       await _unit.Complete();
        return Ok();
    }

    [HttpPut]
    [Route("UpdateLeague")]
    public async Task<IActionResult>Update(LeagueDTO model)
    {
        var league = await _unit.Leagues.FindAsync(x=>x.Id==model.Id);
        if(league is null ) return NotFound();
        league.Name=model.Name;
        _unit.Leagues.Update(league);
        await _unit.Complete();
        return Ok();
    }
}
