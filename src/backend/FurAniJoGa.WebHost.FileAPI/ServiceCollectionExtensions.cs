using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;
using FurAniJoGa.WebHost.FileAPI.Services;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileApi(this IServiceCollection services, 
                                                S3FileServiceOptions s3FileServiceOptions,
                                                RedisSettings redisSettings)
    {
        services.AddControllers();
        services.AddSingleton(s3FileServiceOptions);
        services.AddSingleton(redisSettings);
        services.AddScoped<IFileService, S3FileService>();
        services.AddScoped<IMetadataUploaderService, RedisMetadataUploaderService.RedisMetadataUploaderService>();
        services.AddCors();

        return services;
    }
}