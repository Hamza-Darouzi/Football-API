

namespace Football.Api.Controllers.Public;

[Route("api/public/[controller]")]
[EnableRateLimiting("fixed")]
[ApiController]
[AllowAnonymous]
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

}