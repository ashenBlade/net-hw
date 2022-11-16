using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ConfigurationExtensions
{
    public static S3FileServiceOptions GetS3FileServiceOptions(this ConfigurationManager manager)
    {
        return new S3FileServiceOptions()
               {
                   TempBucket = manager["S3_TEMPORARY_BUCKET"] ?? throw new ArgumentException("S3_TEMPORARY_BUCKET env variable is not provided"),
                   PersistentBucket = manager["S3_PERSISTENT_BUCKET"] ?? throw new ArgumentException("S3_PERSISTENT_BUCKET env variable is not provided"),
                   Host = new Uri(manager["S3_HOST"]) ?? throw new ArgumentException("S3_HOST env variable is not provided"),
                   Password = manager["S3_PASSWORD"] ?? throw new ArgumentException("S3_PASSWORD env variable is not provided"),
                   SecretKey = manager["S3_SECRET"] ?? throw new ArgumentException("S3_SECRET env variable is not provided")
               };
    }
    public static RedisSettings GetRedisSettings(this IConfiguration config)
    {
        return new RedisSettings
        {
            Host = config["REDIS_HOST"] ?? throw new ArgumentException("REDIS_HOST is not provided"),
            Port =
                int.TryParse(config["REDIS_PORT"] ?? throw new ArgumentException("REDIS_PORT is not provided"),
                    out var port)
                    ? port
                    : throw new ArgumentException("Could not parse REDIS_PORT. Must be integer")
        };
    }
}