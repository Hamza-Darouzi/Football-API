namespace FootballAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LeaguesController(ILeagueService leagueService) : ControllerBase
{
    private readonly ILeagueService _leagueService = leagueService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int page=1,int size=10)
    {
        var response = await _leagueService.GetAll(page,size);
        if (response.data is null)
            return NoContent();
        return Ok(response);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _leagueService.Get(id);
        if (response is null)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateLeagueDTO request)
    {
        var response = await _leagueService.Create(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _leagueService.Delete(id);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateLeagueDTO request)
    {
        var response = await _leagueService.Update(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }
}
