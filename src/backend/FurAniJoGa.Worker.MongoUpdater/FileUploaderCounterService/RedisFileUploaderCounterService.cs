using StackExchange.Redis;

namespace FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;

public class RedisFileUploaderCounterService: IFileUploaderCounterService
{
    private readonly RedisSettings _settings;
    public RedisFileUploaderCounterService(RedisSettings settings)
    {
        _settings = settings;
    }


    public async Task<int> IncrementAsync(Guid requestId)
    {
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var newValue = await db.StringIncrementAsync(requestId.ToString());
        return (int) newValue;
    }
}