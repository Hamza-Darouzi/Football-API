
namespace Football.Application.Features.Leagues.Queries.Get;

public record LeaguesGetRequest(int LeagueId) : IRequest<Result>;
