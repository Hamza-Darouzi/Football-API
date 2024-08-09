
namespace Football.Core.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Club> Clubs= new List<Club>();
}
