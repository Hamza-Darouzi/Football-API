
namespace Football.Core.DTOs;

[NotMapped]
public class ClubDTO
{  
     public int Id {get;set;}
     public string Name { get; set; } = string.Empty;
     public DateOnly FoundingDate { get; set; }
     public int LeagueId{ get; set; }
}
