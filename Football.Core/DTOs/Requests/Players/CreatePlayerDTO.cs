
namespace Football.Core.DTOs.Requests.Players;

public record CreatePlayerDTO(string name , string nation , DateOnly birthYear , int clubId);

