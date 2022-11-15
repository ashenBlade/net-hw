namespace FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;

public interface IUploaderService
{
    Task UploadMetadata(Guid requestId, Dictionary<string,string> metadata);
    Task UploadFileId(Guid requestId, Guid fileId);
}