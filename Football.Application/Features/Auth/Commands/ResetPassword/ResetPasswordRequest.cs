


namespace Football.Application.Features.Auth.Commands.ResetPassword;

public record ResetPasswordRequest(string email, string token, string newPassword) : IRequest<Result>;
