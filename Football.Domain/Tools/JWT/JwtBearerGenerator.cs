
#nullable disable

namespace Football.Domain.JWT;

public class JwtBearerGenerator(IOptions<JwtOptions> options, AppDbContext context) : IJwtBearerGenerator
{
    private readonly JwtOptions _jwtOptions = options.Value;
    private readonly AppDbContext _context = context;

    public string Generate(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public string GenerateUniqueRefreshToken()
    {
        var token = GenerateRefreshToken();
        var isUnique = !_context.Users.Any(u=>u.RefreshToken.Token.Equals(token));
        if (!isUnique)
            return GenerateUniqueRefreshToken();
        return token;
    }
    public string GenerateAccessTokenFromRefreshToken(string refreshToken)
    {

        var userClaims = GetUserClaimsFromRefreshToken(refreshToken);
        var accessToken = Generate(userClaims);

        return accessToken;
    }

    private List<Claim> GetUserClaimsFromRefreshToken(string refreshToken)
    {
        // Implement logic to decode the refresh token and retrieve user claims
        // This could involve decrypting, validating, and parsing the refresh token
        // to extract the user's identity and associated claims.

        // For demonstration purposes, let's assume a simple scenario
        // where the refresh token contains user claims directly.
        // In a real-world scenario, this logic would be more complex.
        var userClaims = new List<Claim>();

        // Example: Decode the refresh token (assuming it contains user claims)
        // var decodedClaims = DecodeRefreshToken(refreshToken);

        // Extract user claims from decoded refresh token
        // userClaims.AddRange(decodedClaims);

        // Return user claims
        return userClaims;
    }


}
