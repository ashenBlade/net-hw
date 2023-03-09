using GrpcChat.Auth.Web.Options;

namespace GrpcChat.Auth.Web;

public static class ConfigurationExtensions
{
    public static JwtOptions GetJwtOptions(this IConfiguration configuration) =>
        configuration.GetRequiredSection(JwtOptions.SectionKey)
                     .Get<JwtOptions>();
}