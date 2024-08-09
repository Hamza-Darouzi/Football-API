

namespace FootballAPI.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var response = await _authService.Login(request);

        if (response.data is null)
                return BadRequest(response);

        return Ok(response);
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO request)
    {
        var response = await _authService.Register(request);

        if (response.data is false)
            return BadRequest(response);

        return Ok(response);
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh(RefreshDTO request)
    {
        var response = await _authService.Refresh(request);

        if (response.data is false)
            return BadRequest(response);

        return Ok(response);
    }

}
