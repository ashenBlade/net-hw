using FurAniJoGa.FileAPI.Utility;
using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Infrastructure;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater;

public static class ConfigurationExtensions
{
    public static MongoDbFileMetadataRepositoryOptions GetMongoDbFileMetadataRepositoryOptions(
        this IConfiguration configuration)
    {
        return new MongoDbFileMetadataRepositoryOptions()
               {
                   Collection = configuration["MONGODB_COLLECTION"], 
                   Database = configuration["MONGODB_DATABASE"],
               };
    }

    public static MongoDbConnectionOptions GetMongoDbConnectionOptions(this IConfiguration configuration)
    {
        return new()
               {
                   Host = configuration["MONGODB_HOST"],
                   Password = configuration["MONGODB_PASSWORD"],
                   Port = int.Parse(configuration["MONGODB_PORT"]),
                   Username = configuration["MONGODB_USERNAME"]
               };
    }
    
    
}