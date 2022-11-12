using FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;
using FurAniJoGa.Worker.MongoUpdater.FileMoveService;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater;

public static class ConfigurationExtensions
{
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

    public static MongoSettings GetMongoSettings(this IConfiguration config)
    {
        return new MongoSettings
               {
                   Host = config.GetValue<string>("MONGO_HOST"),
                   Port = config.GetValue<int>("MONGO_PORT"),
                   Username = config.GetValue<string>("MONGO_USERNAME"),
                   Password = config.GetValue<string>("MONGO_PASSWORD"),
                   Database = config.GetValue<string>("MONGO_DATABASE"),
                   Collection = config.GetValue<string>("MONGO_COLLECTION")
               };
    }

    public static S3FileMoveServiceOptions GetS3FileMoveServiceOptions(this IConfiguration configuration)
    {
        return new S3FileMoveServiceOptions()
               {
                   Host = configuration.GetValue<Uri>("S3_HOST"),
                   Password = configuration.GetValue<string>("S3_PASSWORD"),
                   SecretKey = configuration.GetValue<string>("S3_SECRET"),
                   PersistentBucketName = configuration.GetValue<string>("S3_TEMPORARY_BUCKET"),
                   TemporaryBucketName = configuration.GetValue<string>("S3_PERSISTENT_BUCKET")
               };
    }
}