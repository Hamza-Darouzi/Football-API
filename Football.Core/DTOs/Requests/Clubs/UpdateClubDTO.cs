

namespace Football.Core.DTOs.Requests.Clubs;

public record UpdateClubDTO(int id , string name , DateOnly foundingDate , int leagueId);
