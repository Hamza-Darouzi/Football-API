

namespace Football.Application.Features.Players.Queries.Filter;

public record PlayersFilterRequest(int? ClubId,string? Name, DateTime? before, DateTime? after,int page=1,int size=10) : IRequest<Result>;
