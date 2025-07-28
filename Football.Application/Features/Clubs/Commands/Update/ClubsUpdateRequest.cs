


namespace Football.Application.Features.Clubs.Commands.Update;

public record ClubsUpdateRequest(IFormFile? Logo , int ClubId,string Name, DateTime FoundingDate, int LeagueId) : IRequest<Result>;

