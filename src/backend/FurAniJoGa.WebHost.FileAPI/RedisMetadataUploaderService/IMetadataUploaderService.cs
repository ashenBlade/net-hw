namespace FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

public interface IMetadataUploaderService
{
    Task<bool> UploadMetadata(Guid requestId, Dictionary<string,string> metadata);
}