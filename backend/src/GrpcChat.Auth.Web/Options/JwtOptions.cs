using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GrpcChat.Auth.Web.Options;

public class JwtOptions
{
    public const string SectionKey = "JWT";
    [Required]
    [ConfigurationKeyName("AUDIENCE")]
    public string Audience { get; set; } = null!;
    [Required]
    [ConfigurationKeyName("ISSUER")]
    public string Issuer { get; set; } = null!;
    [Required]
    [ConfigurationKeyName("KEY")]
    public string Key { get; set; } = null!;

    public SymmetricSecurityKey CreateSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}