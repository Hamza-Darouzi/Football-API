

namespace Football.Api.Controllers.Public;

[Route("api/public/[controller]")]
[EnableRateLimiting("fixed")]
[ApiController]
[AllowAnonymous]

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

}
