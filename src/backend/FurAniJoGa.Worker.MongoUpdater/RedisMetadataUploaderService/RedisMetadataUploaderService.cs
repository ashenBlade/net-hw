using System.Net.Http.Json;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using StackExchange.Redis;

namespace FurAniJoGa.Worker.MongoUpdater.RedisMetadataUploaderService;

public class RedisMetadataUploaderService : IMetadataUploaderService
{
    private readonly RedisSettings _settings;
    public RedisMetadataUploaderService(RedisSettings settings)
    {
        _settings = settings;
    }


    public async Task<bool> UploadMetadata(Guid requestId, JsonContent metadata)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var result = await db.StringSetAsync(requestId.ToString(), metadata.ToString()).ConfigureAwait(false);
        if (!result)
            throw new InvalidOperationException();
        return result;
    }
}