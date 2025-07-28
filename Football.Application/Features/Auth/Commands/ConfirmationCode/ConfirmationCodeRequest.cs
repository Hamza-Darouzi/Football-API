

namespace Football.Application.Features.Auth.Commands.ConfirmationCode;

public record ConfirmationCodeRequest(string email) : IRequest<Result>;
