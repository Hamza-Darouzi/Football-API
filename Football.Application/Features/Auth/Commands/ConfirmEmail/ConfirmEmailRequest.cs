


namespace Football.Application.Features.Auth.Commands.ConfirmEmail;

public record ConfirmEmailRequest(string email, string token) : IRequest<Result>;
