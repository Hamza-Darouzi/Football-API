
namespace Football.Application.Features.Leagues.Commands.Create;

public record LeaguesCreateRequest(IFormFile? Logo ,string Name):IRequest<Result>;
