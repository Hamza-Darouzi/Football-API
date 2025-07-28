

namespace Football.Application.Features.Clubs.DTOs;

public record ClubsFilterDTO(int Id, string Name, int? LeagueId, string? LeagueName,string? Logo);
