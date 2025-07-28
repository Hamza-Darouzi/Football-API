

namespace Football.Application.Features.Leagues.Queries.Filter;

public record LeaguesFilterRequest(string? Name,int page=1,int size=10) : IRequest<Result>;
