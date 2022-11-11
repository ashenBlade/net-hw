using System.Net.Http.Json;

namespace FurAniJoGa.Worker.MongoUpdater.RedisMetadataUploaderService;

public interface IMetadataUploaderService
{
    Task<bool> UploadMetadata(Guid requestId, JsonContent metadata);
}