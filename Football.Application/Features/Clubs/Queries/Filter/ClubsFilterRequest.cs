

namespace Football.Application.Features.Clubs.Queries.Filter;

public record ClubsFilterRequest(string? Name , DateTime? before, DateTime? after,int page=1,int size=10) : IRequest<Result>;
