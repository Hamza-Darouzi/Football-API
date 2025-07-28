

namespace Football.Application.Features.Leagues.DTOs;

public record LeaguesGetDTO(int Id, string Name,string? Logo, List<LeagueClubsGetDTO> Clubs);
