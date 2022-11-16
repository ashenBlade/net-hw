namespace FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

public interface IUploaderService
{
    Task<bool> UploadMetadata(Guid requestId, Dictionary<string,string> metadata);
    Task<bool> UploadFileId(Guid requestId, Guid fileId);
}