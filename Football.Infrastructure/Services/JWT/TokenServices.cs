

namespace Football.Infrastructure.Services.JWT;

public class TokenServices(
    IJwtProvider jwtProvider,
    AppDbContext context
)
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly AppDbContext _context = context;

    public async Task<TokenResponse> GenerateToken(
        User user,
        List<string> roles,
        CancellationToken cancellationToken
    )
    {
        var Token = _jwtProvider.Generate(user, roles);

        Random r = new Random();

        var RefreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid(),
            ExpiryDate = DateTime.UtcNow.AddMonths(1),
            UserId = user.Id
        };
        await _context.RefreshTokens.AddAsync(RefreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new TokenResponse(
            new JwtSecurityTokenHandler().WriteToken(Token),
            RefreshToken.Token.ToString()
        );
    }
}