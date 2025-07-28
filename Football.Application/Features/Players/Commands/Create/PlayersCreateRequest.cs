

namespace Football.Application.Features.Players.Commands.Create;

public record PlayersCreateRequest(IFormFile? Image,int ClubId , string Name , string Nation , DateTime BirthYear ):IRequest<Result>;
