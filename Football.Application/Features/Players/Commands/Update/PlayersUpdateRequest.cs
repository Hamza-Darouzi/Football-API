

namespace Football.Application.Features.Players.Commands.Update;

public record PlayersUpdateRequest(IFormFile? Image , int playerId , int ClubId, string Name, string Nation, DateTime BirthYear) : IRequest<Result>;

