

namespace Football.Application.Features.Clubs.Commands.Delete;

public record ClubsDeleteRequest(int ClubId) : IRequest<Result>;
