namespace Football.Infrastructure.Services.JWT;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public SecurityToken Generate(User user, List<string> roles)
    {
        var JwtTokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var subject = new ClaimsIdentity(claims);

        subject.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        subject.AddClaim(
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        );

        var Key = Encoding.UTF8.GetBytes(_options.Secret!);
        var TokenDescripter = new SecurityTokenDescriptor()
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            Subject = subject,
            Expires = DateTime.UtcNow.AddDays(30),
            //Expires = DateTime.UtcNow.Add(_options.ExpireyTimeFrame),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256
            ),
        };

        return JwtTokenHandler.CreateToken(TokenDescripter);
    }
}
