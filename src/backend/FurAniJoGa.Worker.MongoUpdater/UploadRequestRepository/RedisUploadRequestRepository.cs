using System.Text.Json;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using StackExchange.Redis;

namespace FurAniJoGa.Worker.MongoUpdater.UploadRequestRepository;

public class RedisUploadRequestRepository : IUploadRequestRepository
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
            if (!value.HasValue)
            {
                _logger.LogWarning("Could not find FileId for request {RequestId}: value is null or empty", requestId);
                return null;
            }
            var valueString = (string?) value;
            if (Guid.TryParse(valueString, out var parsedFileId))
                return parsedFileId;

            _logger.LogWarning("Could not parse returned value ({ReturnedValue}) to Guid", valueString);
            return null;
        }

        Dictionary<string, object>? GetMetadata(RedisValue value)
        {
            if (value.IsNullOrEmpty)
            {
                _logger.LogWarning("Could not find metadata for request: {RequestId}. Returned null or empty",
                                   requestId);
                return null;
            }

            var valueString = (string?) value;
            if (valueString is null)
            {
                _logger.LogWarning("Found metadata is null");
                return null;
            }

            try
            {
                var metadataDeserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(valueString);
                if (metadataDeserialized is null)
                {
                    _logger.LogWarning("Deserialized metadata is null. Returned value: {ReturnedValue}", valueString);
                    return null;
                }

                return metadataDeserialized;
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
        var results = await db.StringGetAsync(new RedisKey[] {
            $"{requestId.ToString()}-fileId", 
            $"{requestId.ToString()}-metadata"
        });
        
        var (fileId, metadata) = ( GetFileId(results[0]), GetMetadata(results[1]) );
        if (fileId is not null && metadata is not null)
        {
            return Tuple.Create(fileId.Value, metadata);
        }

        return null;
    }
}