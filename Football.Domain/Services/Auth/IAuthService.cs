
namespace Football.Domain.Services.Auth;

public interface IAuthService
{
    Task<Result> Login(LoginDTO request);
    Task<Result> Register(RegisterDTO request);
    Task<Result> Refresh(RefreshDTO request);
}
