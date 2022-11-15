using System.Text.Json;
using StackExchange.Redis;

namespace FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

public class RedisUploaderService : IUploaderService
{
    private readonly RedisSettings _settings;
    public RedisUploaderService(RedisSettings settings)
    {
        _settings = settings;
    }


    public async Task UploadMetadata(Guid requestId, Dictionary<string,string> metadata)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        
        var value = await db.HashGetAllAsync(requestId.ToString());

        var metadataString = JsonSerializer.Serialize(metadata);
        
        if (value.Length == 0)
        {
            HashEntry[] redisHash = {
                new HashEntry("fileId", ""),
                new HashEntry("metadata", metadataString)
            };
            await db.HashSetAsync(requestId.ToString(), redisHash);
        }

        await db.HashSetAsync(requestId.ToString(), "metadata", metadataString);
    }

    public async Task UploadFileId(Guid requestId, Guid fileId)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        
        var value = await db.HashGetAllAsync(requestId.ToString());

        if (value.Length == 0)
        {
            HashEntry[] redisHash = {
                new HashEntry("fileId", fileId.ToString()),
                new HashEntry("metadata", "")
            };
            await db.HashSetAsync(requestId.ToString(), redisHash);
        }

        await db.HashSetAsync(requestId.ToString(), "fileId", fileId.ToString());
    }
}