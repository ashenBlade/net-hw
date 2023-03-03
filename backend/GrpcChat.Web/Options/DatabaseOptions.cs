using System.ComponentModel.DataAnnotations;

namespace GrpcChat.Web.Options;

public class DatabaseOptions
{
    public const string Key = "DATABASE";
    [Required]
    [ConfigurationKeyName("CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
}