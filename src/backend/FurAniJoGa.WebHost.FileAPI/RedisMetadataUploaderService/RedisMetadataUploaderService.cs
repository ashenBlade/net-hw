using System.Text.Json;
using StackExchange.Redis;

namespace FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

public class RedisMetadataUploaderService : IMetadataUploaderService
{
    private readonly RedisSettings _settings;
    public RedisMetadataUploaderService(RedisSettings settings)
    {
        _settings = settings;
    }


    public async Task<bool> UploadMetadata(Guid requestId, Dictionary<string,string> metadata)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var result = await db.StringSetAsync(requestId.ToString(), JsonSerializer.Serialize(metadata)).ConfigureAwait(false);
        if (!result)
            throw new InvalidOperationException();
        return result;
    }
}