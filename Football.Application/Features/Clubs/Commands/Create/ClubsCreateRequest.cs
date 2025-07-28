

namespace Football.Application.Features.Clubs.Commands.Create;

public record ClubsCreateRequest(IFormFile? logo , string Name , DateTime FoundingDate , int LeagueId):IRequest<Result>;
