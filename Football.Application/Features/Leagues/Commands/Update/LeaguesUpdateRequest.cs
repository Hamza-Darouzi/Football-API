

namespace Football.Application.Features.Leagues.Commands.Update;

public record LeaguesUpdateRequest(IFormFile? Logo , int LeagueId,string Name) : IRequest<Result>;

