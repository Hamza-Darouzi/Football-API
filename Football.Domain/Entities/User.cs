



namespace Football.Domain.Entities;

public class User:IdentityUser<int>
{
    public string Username { get; set; } = null!;
    public string? ResetToken { get; set; } 
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}