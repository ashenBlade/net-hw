using System.Text.Json;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MongoDB.Bson;
using StackExchange.Redis;
using ZstdSharp.Unsafe;

namespace FurAniJoGa.Worker.MongoUpdater.FileIdRepository;

public class RedisUploadRequestRepository: IUploadRequestRepository
{
    private readonly RedisSettings _settings;
    private readonly ILogger<RedisUploadRequestRepository> _logger;

    public RedisUploadRequestRepository(RedisSettings settings, ILogger<RedisUploadRequestRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }
    
    public async Task<Tuple<Guid, Dictionary<string, object>>?> FindFileIdAsync(Guid requestId)
    {
        Guid? GetFileId(RedisValue value)
        {
            if (value.IsNullOrEmpty)
            {
                _logger.LogWarning("Could not find FileId for request {RequestId}: value is null or empty", requestId);
                return null;
            }

            var valueString = value.Box()!.ToString();
            if (Guid.TryParse(valueString, out var fileId)) 
                return fileId;
            
            _logger.LogWarning("Could not parse returned value ({ReturnedValue}) to Guid", valueString);
            return null;
        }

        Dictionary<string, object>? GetMetadata(RedisValue value)
        {
            if (value.IsNullOrEmpty)
            {
                _logger.LogWarning("Could not find metadata for request: {RequestId}. Returned null or empty", requestId);
                return null;
            }

            var valueString = value.Box()!.ToString()!;
            try
            {
                var metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(valueString)!;
                if (metadata is null)
                {
                    _logger.LogWarning("Deserialized metadata is null. Returned value: {ReturnedValue}", valueString);
                    return null;
                }

                return metadata;
            }
            catch (JsonException json)
            {
                _logger.LogWarning(json, 
                                   "Could not deserialize found metadata string for request: {RequestId}. Returned value: {ReturnedValue}", 
                                   requestId, 
                                   valueString);
                return null;
            }
        }
        
        var multiplexer = await ConnectionMultiplexer.ConnectAsync($"{_settings.Host}:{_settings.Port}");
        var db = multiplexer.GetDatabase();
        var results = await Task.WhenAll(db.StringGetAsync($"{requestId}-fileId"), db.StringGetAsync($"{requestId}-metadata"));
        var (fileId, metadata) = (GetFileId(results[0]), GetMetadata(results[1]));
        if (fileId is not null && metadata is not null)
        {
            return Tuple.Create(fileId.Value, metadata);
        }

        return null;
    }
}