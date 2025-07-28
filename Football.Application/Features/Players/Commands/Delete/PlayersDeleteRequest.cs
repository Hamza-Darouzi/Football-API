

namespace Football.Application.Features.Players.Commands.Delete;

public record PlayersDeleteRequest(int playerId) : IRequest<Result>;
