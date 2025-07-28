

namespace Football.Application.Features.Players.DTOs;

public record PlayerFilterDTO(int Id, int? ClubId, string ClubName, string Name, string Nation, int Age,string? image);
