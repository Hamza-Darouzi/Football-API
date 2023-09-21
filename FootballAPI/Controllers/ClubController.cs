


namespace FootballAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClubController: ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;
    public ClubController(IUnitOfWork unit , IMapper mapper)
    { 
        _unit = unit;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllClubs")]
    public async Task<IActionResult> GetAll()
    {
        var clubs = await _unit.Clubs.GetAllAsync();
        if(clubs.Count==0) return NoContent();
        return Ok(clubs);
    }
    
    [HttpGet]
    [Route("GetClub")]
    public async Task<IActionResult> GetById(int id)
    {
        var club = await _unit.Clubs.FindAsync(x=>x.Id==id,new[]{"Players"});
        if(club is  null)
            return NotFound();
        return Ok(club);

    }

    [HttpPost]
    [Route("AddClub")]
    public async Task<IActionResult> Add(ClubDTO model)
    {
        if(model is null) return NotFound();
        
        var club = _mapper.Map<Club>(model);
        await _unit.Clubs.Create(club);
        await _unit.Complete();
        return Ok(club);
    }
    
    [HttpDelete]
    [Route("DeleteClub")]
    public async Task<IActionResult>Delete(int id)
    {
        var club = await _unit.Clubs.FindAsync(x=>x.Id==id);
        if(club is null ) return NotFound();
        
       _unit.Clubs.Delete(club);
       await _unit.Complete();
        return Ok();
    }

    [HttpPut]
    [Route("UpdateClub")]
    public async Task<IActionResult>Update(ClubDTO model)
    {
        var club = await _unit.Clubs.FindAsync(x=>x.Id==model.Id);
        if(club is null ) return NotFound();
        club.Name=model.Name;
        club.LeagueId=model.LeagueId;
        club.FoundingDate = model.FoundingDate;
        _unit.Clubs.Update(club);
        await _unit.Complete();
        return Ok();
    }
    
}
