using FurAniJoGa.WebHost.FileAPI.Services;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileApi(this IServiceCollection services, 
                                                S3FileServiceOptions s3FileServiceOptions)
    {
        services.AddControllers();
        services.AddScoped<IFileService, S3FileService>();
        services.AddSingleton(s3FileServiceOptions);

        return services;
    }
}