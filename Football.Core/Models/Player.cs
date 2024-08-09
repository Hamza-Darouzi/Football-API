
namespace Football.Core.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nation { get; set; } = string.Empty;

    public DateOnly BirthYear { get; set; }

    [NotMapped]
    public int Age => (DateTime.Now.Year-BirthYear.Year);
    public  int? ClubId { get; set; }
    public  Club? Club { get; set; }
    
}
