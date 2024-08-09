namespace FootballAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PlayerController(IPlayersService playersService) : ControllerBase
{
    private readonly IPlayersService _playersService = playersService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int page, int size)
    {
        var response = await _playersService.GetAll(page,size);
        if (response.data is null)
            return NoContent();
        return Ok(response);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _playersService.Get(id);
        if (response is null)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreatePlayerDTO request)
    {
        var response = await _playersService.Create(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _playersService.Delete(id);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdatePlayerDTO request)
    {
        var response = await _playersService.Update(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

}
