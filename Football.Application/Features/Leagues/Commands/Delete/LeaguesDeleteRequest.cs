

namespace Football.Application.Features.Leagues.Commands.Delete;

public record LeaguesDeleteRequest(int LeagueId) : IRequest<Result>;
