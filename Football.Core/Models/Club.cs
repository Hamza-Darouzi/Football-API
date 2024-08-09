

namespace Football.Core.Models;

public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public DateOnly? FoundingDate { get; set; }
    public  int? LeagueId { get; set; }
    public  League? League { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
}
