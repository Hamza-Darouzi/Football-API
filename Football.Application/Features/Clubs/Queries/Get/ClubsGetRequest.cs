

namespace Football.Application.Features.Clubs.Queries.Get;

public record ClubsGetRequest(int ClubId) : IRequest<Result>;
