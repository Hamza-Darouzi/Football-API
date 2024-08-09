
namespace FootballAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClubController(IClubService clubService): ControllerBase
{
    private readonly IClubService _clubService = clubService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int page=1,int size=10)
    {
        var response = await _clubService.GetAll(page,size);
        if(response.data is null) 
            return NoContent();
        return Ok(response);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _clubService.Get(id);
        if(response is null)
            return BadRequest(response);
        return Ok(response);

    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateClubDTO request)
    {
        var response = await _clubService.Create(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult>Delete(int id)
    {
        var response = await _clubService.Delete(id);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult>Update(UpdateClubDTO request)
    {
        var response = await _clubService.Update(request);
        if (response.data is false)
            return BadRequest(response);
        return Ok(response);
    }
    
}
