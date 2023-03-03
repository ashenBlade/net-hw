namespace GrpcChat.TokenGenerator.Jwt;

public class JwtTokenOptions
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
    public TimeSpan Lifetime { get; set; }
}