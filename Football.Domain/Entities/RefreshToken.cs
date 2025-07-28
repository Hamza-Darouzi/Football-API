

namespace Football.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public Guid Token { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(30);
}