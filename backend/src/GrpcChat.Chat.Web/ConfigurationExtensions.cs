using GrpcChat.Chat.Web.Options;

namespace GrpcChat.Chat.Web;

public static class ConfigurationExtensions
{
    public static JwtOptions GetJwtOptions(this IConfiguration configuration) =>
        configuration.GetRequiredSection(JwtOptions.SectionKey)
                     .Get<JwtOptions>();
}