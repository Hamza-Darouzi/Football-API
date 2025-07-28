namespace Football.Application.Features.Auth.Commands.Login;

public record LoginRequest(string email, string password) : IRequest<Result>;
