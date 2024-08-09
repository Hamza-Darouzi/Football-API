

namespace Football.Core.Models;

[ComplexType]
public class RefreshToken
{
    public string Token { get; set; }
    public DateTime ExpireAt { get; set; } 
}
