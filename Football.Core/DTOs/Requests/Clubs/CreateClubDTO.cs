

namespace Football.Core.DTOs.Requests.Clubs;

public record CreateClubDTO(string name , DateOnly foundingDate , int leagueId);
