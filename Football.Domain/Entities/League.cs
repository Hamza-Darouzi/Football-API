
namespace Football.Domain.Entities;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string? Logo { get; set; } 
    public virtual ICollection<Club> Clubs { get; set; }
    public League()
    {
        
    }
    public League(string name)
    {
        Name = name;
    }
}
