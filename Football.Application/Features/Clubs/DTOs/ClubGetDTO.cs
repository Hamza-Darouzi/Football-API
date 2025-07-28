

namespace Football.Application.Features.Clubs.DTOs;

public record ClubGetDTO(int Id, string Name, DateTime FoundingDate, int LeagueId, string LeagueName,string? Logo, List<ClubsPlayersGetDTO> Players);
