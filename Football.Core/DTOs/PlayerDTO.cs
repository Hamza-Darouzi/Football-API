
namespace Football.Core.DTOs;

[NotMapped]
public class PlayerDTO
{
    public string Name { get; set; } = string.Empty;
    public string Nation { get; set; } = string.Empty;
    public DateOnly BirthYear { get; set; }
    public int ClubId { get; set; }
}
