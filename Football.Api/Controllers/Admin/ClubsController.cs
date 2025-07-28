

namespace Football.Api.Controllers.Admin;

[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class ClubsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] ClubsFilterRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return NoContent();
        return Ok(response);
    }
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromQuery] ClubsGetRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromForm] ClubsCreateRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm] ClubsUpdateRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] ClubsDeleteRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.error != Error.None)
            return BadRequest(response);
        return Ok(response);
    }
}