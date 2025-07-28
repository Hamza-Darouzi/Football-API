

namespace Football.Api.Controllers.Admin;

[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeaguesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] LeaguesFilterRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return NoContent();
        return Ok(response);
    }
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromQuery] LeaguesGetRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromForm] LeaguesCreateRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm] LeaguesUpdateRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] LeaguesDeleteRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }
}
