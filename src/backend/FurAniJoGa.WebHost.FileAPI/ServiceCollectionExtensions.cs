using FurAniJoGa.WebHost.FileAPI.Services;
using FurAniJoGa.Worker.MongoUpdater.RedisMetadataUploaderService;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileApi(this IServiceCollection services, 
                                                S3FileServiceOptions s3FileServiceOptions)
    {
        services.AddControllers();
        services.AddSingleton(s3FileServiceOptions);
        services.AddScoped<IFileService, S3FileService>();
        services.AddScoped<IMetadataUploaderService, RedisMetadataUploaderService>();
        services.AddCors();

        return services;
    }
}