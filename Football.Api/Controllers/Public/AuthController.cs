namespace Football.Api.Controllers.Public;

[Route("api/public/[controller]")]
[ApiController]
[AllowAnonymous]
[EnableRateLimiting("auth")]

public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is null)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is false)
            return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmPassword(ConfirmEmailRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is false)
            return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("ConfirmationCode")]
    public async Task<IActionResult> ConfirmPassword(ConfirmationCodeRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is false)
            return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is false)
            return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var result = await _mediator.Send(request);
        if (result.data is false)
            return BadRequest(result);
        return Ok(result);
    }
}
