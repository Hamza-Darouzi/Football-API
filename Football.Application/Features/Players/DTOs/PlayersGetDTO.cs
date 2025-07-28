

namespace Football.Application.Features.Players.DTOs;

public record PlayersGetDTO(int Id, string Name, string Nation, int? ClubId, string ClubName, DateTime BirthYear,string? image);
