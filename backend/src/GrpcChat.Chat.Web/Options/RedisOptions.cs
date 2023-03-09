using System.ComponentModel.DataAnnotations;

namespace GrpcChat.Chat.Web.Options;

public class RedisOptions
{
    public const string Key = "REDIS";
    [Required]
    [ConfigurationKeyName("SERVERS")]
    public string Servers { get; set; } = null!;

    [Required]
    [ConfigurationKeyName("CHANNEL")]
    public string Channel { get; set; } = null!;
}