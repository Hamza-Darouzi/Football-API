

namespace Football.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Nation { get; set; } 
    public string? Image { get; set; } 

    public DateTime BirthDay { get; set; }

    public  int? ClubId { get; set; }
    public  virtual Club? Club { get; set; }

    public Player()
    {
        
    }

    public Player(int clubId , string name , string nation , DateTime birthDay)
    {
        ClubId = clubId;
        Name = name;
        Nation = nation;
        BirthDay = birthDay;
    }

}
