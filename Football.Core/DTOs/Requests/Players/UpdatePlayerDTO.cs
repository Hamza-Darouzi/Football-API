namespace Football.Core.DTOs.Requests.Players;

public record UpdatePlayerDTO(int id , string name , string nation , DateOnly birthYear , int clubId);

