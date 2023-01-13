using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.JwtService;

public class SimpleJwtService: IJwtService
{
    public static readonly SymmetricSecurityKey SecurityKey = new(new byte[500]);
    public SecurityKey Key => SecurityKey;
    public string CreateJwt(User user)
    {
        var claims = new List<Claim>{ new(JwtRegisteredClaimNames.NameId, user.Id.ToString()) };

        var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
                              {
                                  Subject = new ClaimsIdentity(claims),
                                  Expires = DateTime.Now.AddDays(7),
                                  SigningCredentials = credentials
                              };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}