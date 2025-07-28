



namespace Football.Application.Features.Auth.Commands.ForgetPassword;

public record ForgetPasswordRequest(string email) : IRequest<Result>;
