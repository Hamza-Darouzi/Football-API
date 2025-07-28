
namespace Football.Application.Features.Players.Queries.Get;

public record PlayersGetRequest(int playerId) : IRequest<Result>;
