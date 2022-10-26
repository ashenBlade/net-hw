using FurAniJoGa.FileAPI.Abstractions;
using FurAniJoGa.FileAPI.Utility.S3FileService;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileApi(this IServiceCollection services, 
                                                S3FileServiceOptions s3FileServiceOptions)
    {
        services.AddControllers();
        services.AddSingleton(s3FileServiceOptions);
        services.AddScoped<IFileService, S3FileService>();
        services.AddCors();

        return services;
    }
}