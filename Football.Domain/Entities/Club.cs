

namespace Football.Domain.Entities;

public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string? Logo { get; set; } 

    public DateTime FoundingDate { get; set; }
    public int LeagueId { get; set; }
    public virtual League? League { get; set; }
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public Club()
    {
        
    }
    public Club(string name , DateTime foundingDate , int leagueId)
    {
        Name = name;
        FoundingDate = foundingDate;
        LeagueId = leagueId;
    }
}
