

namespace Football.Domain.JWT;

public interface IJwtBearerGenerator
{
    string Generate(List<Claim> claims);
    string GenerateRefreshToken();
    public string GenerateUniqueRefreshToken();
    string GenerateAccessTokenFromRefreshToken(string refreshToken);
}
