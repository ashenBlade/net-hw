using Microsoft.IdentityModel.Tokens;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.JwtService;

public interface IJwtService
{
    SecurityKey Key { get; }
    string CreateJwt(User user);
}