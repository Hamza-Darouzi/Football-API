namespace Football.Core.Options;

public class JwtOptions 
{
    public const string Jwt = "jwt";

    public string Key { get; set; } = string.Empty;
    public TimeSpan Expire { get; set; }
}