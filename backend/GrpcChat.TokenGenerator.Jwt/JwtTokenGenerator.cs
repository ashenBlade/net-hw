using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GrpcChat.TokenGenerator.Jwt;

public class JwtTokenGenerator: ITokenGenerator
{
    private readonly JwtTokenOptions _options;
    private readonly Lazy<SigningCredentials> _lazyCredentials;

    public JwtTokenGenerator(JwtTokenOptions options)
    {
        _options = options;
        _lazyCredentials = new Lazy<SigningCredentials>(() => new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)), 
                                                                                     SecurityAlgorithms.HmacSha512Signature));
    }

    public string GenerateToken(string subject)
    {
        var tokenDescriptor = new SecurityTokenDescriptor()
                              {
                                  Subject = new ClaimsIdentity(new List<Claim>()
                                                               {
                                                                   new(JwtRegisteredClaimNames.NameId, subject),
                                                               }),
                                  Expires = DateTime.Now.Add(_options.Lifetime),
                                  Audience = _options.Audience,
                                  Issuer = _options.Issuer,
                                  SigningCredentials = _lazyCredentials.Value,
                              };
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(handler.CreateToken(tokenDescriptor));
    }
}