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


    public async Task<bool> UploadMetadata(Guid requestId, Dictionary<string,string> metadata)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var result = await db.StringSetAsync($"{requestId.ToString()}-metadata", JsonSerializer.Serialize(metadata)).ConfigureAwait(false);
        if (!result)
            throw new InvalidOperationException();
        return result;
    }

    public async Task<bool> UploadFileId(Guid requestId, Guid fileId)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var result = await db.StringSetAsync($"{requestId.ToString()}-fileId", fileId.ToString()).ConfigureAwait(false);
        if (!result)
            throw new InvalidOperationException();
        return result;
    }
}