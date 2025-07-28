


namespace Football.Infrastructure.Services.JWT;

public interface IJwtProvider
{
    SecurityToken Generate(User user, List<string> roles);
}
