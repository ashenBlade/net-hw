using GrpcChat.Web.Options;

namespace GrpcChat.Web;

public static class ConfigurationExtensions
{
    public static JwtOptions GetJwtOptions(this IConfiguration configuration) =>
        configuration.GetRequiredSection(JwtOptions.SectionKey)
                     .Get<JwtOptions>();
}