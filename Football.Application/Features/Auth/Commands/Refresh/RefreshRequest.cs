
namespace Football.Application.Features.Auth.Commands.Refresh;

public record RefreshRequest(string accessToken, string refreshToken) : IRequest<Result>;
